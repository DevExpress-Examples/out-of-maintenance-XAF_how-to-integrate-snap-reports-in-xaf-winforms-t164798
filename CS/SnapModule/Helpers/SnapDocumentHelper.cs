using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using SnapModule.Base;
using DevExpress.Snap;
using DevExpress.XtraPrinting;
using System.IO;
using DevExpress.Snap.Core.API;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using System.Data;
using System.Collections;
using DevExpress.ExpressApp.SystemModule;
using SnapModule.Editors;

namespace SnapModule.Helpers {
    public class SnapDocumentHelper {
        private IDocumentData documentData;
        private IObjectSpace objectSpace;
        private XafApplication application;
        private CriteriaOperator inPlaceCriteria;
        private SnapDocument document;

        public SnapDocumentHelper(IDocumentData documentData, IObjectSpace objectSpace, XafApplication application)
            : this(documentData, objectSpace, application, null) { }

        public SnapDocumentHelper(IDocumentData documentData, IObjectSpace objectSpace, XafApplication application, CriteriaOperator inPlaceCriteria) {
            this.documentData = documentData;
            this.objectSpace = objectSpace;
            this.application = application;
            this.inPlaceCriteria = inPlaceCriteria;
        }

        public static void ShowDesigner(IDocumentData documentData, IObjectSpace objectSpace, XafApplication application) {
            SnapDocumentHelper helper = new SnapDocumentHelper(documentData, objectSpace, application);
            SnapDesigner designer = new SnapDesigner(helper);
            designer.ShowDialog();
        }
        public static void ShowPrintPreview(IDocumentData documentData, IObjectSpace objectSpace) {
            ShowPrintPreview(documentData, objectSpace, null);
        }
        public static void ShowPrintPreview(IDocumentData documentData, IObjectSpace objectSpace, CriteriaOperator inPlaceCriteria) {
            using (SnapDocumentServer server = new SnapDocumentServer()) {
                SnapDocumentHelper helper = new SnapDocumentHelper(documentData, objectSpace, null, inPlaceCriteria);
                helper.LoadDocument(server.Document);
                // Mail Merge
                if (server.Document.DataSources.Count == 1 && server.Options.SnapMailMergeVisualOptions.DataSource == server.Document.DataSources[0].DataSource) {
                    using (MemoryStream stream = new MemoryStream()) {
                        server.SnapMailMerge(stream, SnapDocumentFormat.Snap);
                        stream.Flush();
                        stream.Seek(0, SeekOrigin.Begin);
                        server.LoadDocument(stream, SnapDocumentFormat.Snap);
                    }
                }
                using (PrintingSystem ps = new PrintingSystem()) {
                    PrintableComponentLink link = new PrintableComponentLink(ps);
                    link.Component = server;
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
            }
        }
        public void LoadDocument(SnapDocument document) {
            this.document = document;
            if (documentData.Content != null) {
                using (MemoryStream stream = new MemoryStream()) {
                    stream.Write(documentData.Content, 0, documentData.Content.Length);
                    stream.Flush();
                    stream.Seek(0, SeekOrigin.Begin);
                    document.LoadDocument(stream, SnapDocumentFormat.Snap);
                }
            }
            SynchronizeDataSources();
        }

        private void SynchronizeDataSources() {
            List<DataSourceInfo> dataSourcesToRemove = new List<DataSourceInfo>();
            foreach (DataSourceInfo dataSourceInfo in document.DataSources) {
                if (!(dataSourceInfo.DataSource is DataSet)) {
                    bool exists = false;
                    foreach (IDocumentDataSource dataSource in documentData.DataSources) {
                        if (dataSource.Name == dataSourceInfo.DataSourceName) {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) {
                        dataSourcesToRemove.Add(dataSourceInfo);
                    }
                }
            }
            foreach (DataSourceInfo dataSourceInfo in dataSourcesToRemove) {
                document.DataSources.Remove(dataSourceInfo);
            }
            foreach (IDocumentDataSource dataSource in documentData.DataSources) {
                AddDataSource(dataSource, dataSource == documentData.InplaceDocumentDataSource);
            }
            document.Fields.Update();
        }
        private void AddDataSource(IDocumentDataSource dataSource, bool isInplaceDataSource) {
            CriteriaOperator criteria = isInplaceDataSource ? inPlaceCriteria : null;
            if (!String.IsNullOrEmpty(dataSource.Criteria)) {
                CriteriaOperator dataSourceCriteria = CriteriaEditorHelper.GetCriteriaOperator(dataSource.Criteria, dataSource.DataType, objectSpace);
                if (ReferenceEquals(criteria, null)) {
                    criteria = dataSourceCriteria;
                } else {
                    criteria = new GroupOperator(GroupOperatorType.And, criteria, dataSourceCriteria);
                }
            }
            CollectionSource collectionSource = new CollectionSource(objectSpace, dataSource.DataType, CollectionSourceDataAccessMode.Client, CollectionSourceMode.Proxy);
            collectionSource.Criteria["SnapDataSourceFiler"] = criteria;
            if (document.DataSources[dataSource.Name] != null) {
                document.DataSources[dataSource.Name].DataSource = collectionSource.Collection;
            } else {
                document.DataSources.Add(dataSource.Name, collectionSource.Collection);
            }
        }
        internal void SaveDocument() {
            if (objectSpace.IsNewObject(documentData)) {
                ShowDocumentDataObject();
            }
            using (MemoryStream stream = new MemoryStream()) {
                document.SaveDocument(stream);
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                documentData.Content = bytes;
                objectSpace.CommitChanges();
            }
        }
        internal void ShowDocumentDataObject() {
            DetailView documentDetailView = application.CreateDetailView(objectSpace, documentData, false);
            documentDetailView.Caption = "Save Document";
            ShowViewParameters svp = new ShowViewParameters(documentDetailView);
            svp.TargetWindow = TargetWindow.NewModalWindow;
            svp.Context = TemplateContext.PopupWindow;
            DialogController dc = application.CreateController<DialogController>();
            dc.SaveOnAccept = false;
            svp.Controllers.Add(dc);
            application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
            SynchronizeDataSources();
        }
        internal void CreateNewDocument(SnapDocument document) {
            documentData = (IDocumentData)objectSpace.CreateObject(documentData.GetType());
        }
        internal void ShowNewDataSourceDialog(){
            IObjectSpace newDataSourceObjectSpace = objectSpace.CreateNestedObjectSpace();
            Type dataSourceType = XafTypesInfo.Instance.FindTypeInfo(documentData.GetType()).FindMember("DataSources").ListElementType;
            IDocumentDataSource dataSource = (IDocumentDataSource)newDataSourceObjectSpace.CreateObject(dataSourceType);
            DetailView dataSourceDetailView = application.CreateDetailView(newDataSourceObjectSpace, dataSource);
            ShowViewParameters svp = new ShowViewParameters(dataSourceDetailView);
            svp.TargetWindow = TargetWindow.NewModalWindow;
            svp.Context = TemplateContext.PopupWindow;
            DialogController dc = application.CreateController<DialogController>();
            svp.Controllers.Add(dc);
            dc.Accepting += delegate {
                newDataSourceObjectSpace.CommitChanges();
                IDocumentDataSource newDataSource = (IDocumentDataSource)objectSpace.GetObject(dataSource);
                documentData.DataSources.Add(newDataSource);
                AddDataSource(newDataSource, false);
            };
            application.ShowViewStrategy.ShowView(svp, new ShowViewSource(null, null));
        }
        internal bool QueryCanClose() {
            ConfirmationResult result = application.AskConfirmation(ConfirmationType.NeedSaveChanges);
            if (result == ConfirmationResult.Cancel) {
                return false;
            }
            if (result == ConfirmationResult.Yes) {
                SaveDocument();
            }
            return true;
        }
        internal string GetDocumentName() {
            return documentData.DocumentName;
        }
        internal bool Modified {
            get {
                return objectSpace.IsModified;
            }
        }
    }
}
