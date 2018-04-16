using System;
using System.Collections.Generic;
using DevExpress.ExpressApp.Utils;
using SnapModule.Base;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections;

namespace SnapModule.Helpers {
    public class InplaceDocumentsCacheHelper {
        private IObjectSpace objectSpace;
        private Type documentDataType;
        public InplaceDocumentsCacheHelper(IObjectSpace objectSpace, Type documentDataType) {
            this.objectSpace = objectSpace;
            this.documentDataType = documentDataType;
        }
        private bool isCompleteCache = false;
        private LightDictionary<Type, List<IDocumentData>> inplaceDocumentsCache = new LightDictionary<Type, List<IDocumentData>>();
        public virtual List<IDocumentData> GetDocumentDataList(Type targetObjectType) {
            EnsureCache();
            List<IDocumentData> cachedDocuments = new List<IDocumentData>();
            foreach (Type key in inplaceDocumentsCache.Keys) {
                if (key.IsAssignableFrom(targetObjectType)) {
                    cachedDocuments.AddRange(inplaceDocumentsCache[key]);
                }
            }
            return cachedDocuments;
        }
        public void ClearInplaceDocumentsCache() {
            isCompleteCache = false;
            inplaceDocumentsCache.Clear();
        }
        protected virtual IList CollectAllInplaceDocumentsData() {
            return objectSpace.GetObjects(documentDataType, CriteriaOperator.Parse("InplaceDocumentDataSource is not null"));
        }
        private void EnsureCache() {
            if (!isCompleteCache) {
                IList allInPlaceDocumentsData = CollectAllInplaceDocumentsData();
                foreach (IDocumentData item in allInPlaceDocumentsData) {
                    if (item.InplaceDocumentDataSource != null) {
                        List<IDocumentData> items;
                        if (!inplaceDocumentsCache.TryGetValue(item.InplaceDocumentDataSource.DataType, out items)) {
                            items = new List<IDocumentData>();
                            inplaceDocumentsCache[item.InplaceDocumentDataSource.DataType] = items;
                        }
                        items.Add(item);
                    }
                }
                isCompleteCache = true;
            }
        }
    }
}
