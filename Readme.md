<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128591951/15.1.9%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T164798)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [DocumentData.cs](./CS/SnapExample.Module/BusinessObjects/DocumentData.cs) (VB: [DocumentDataSource.vb](./VB/SnapExample.Module/BusinessObjects/DocumentDataSource.vb))
* [DocumentDataSource.cs](./CS/SnapExample.Module/BusinessObjects/DocumentDataSource.cs) (VB: [DocumentDataSource.vb](./VB/SnapExample.Module/BusinessObjects/DocumentDataSource.vb))
* [IDocumentData.cs](./CS/SnapModule/Base/IDocumentData.cs) (VB: [IDocumentDataSource.vb](./VB/SnapModule/Base/IDocumentDataSource.vb))
* [IDocumentDataSource.cs](./CS/SnapModule/Base/IDocumentDataSource.cs) (VB: [IDocumentDataSource.vb](./VB/SnapModule/Base/IDocumentDataSource.vb))
* [DocumentDataController.cs](./CS/SnapModule/Controllers/DocumentDataController.cs) (VB: [DocumentDataController.vb](./VB/SnapModule/Controllers/DocumentDataController.vb))
* [InplaceDocumentsController.cs](./CS/SnapModule/Controllers/InplaceDocumentsController.cs) (VB: [InplaceDocumentsController.vb](./VB/SnapModule/Controllers/InplaceDocumentsController.vb))
* [SnapDesigner.cs](./CS/SnapModule/Editors/SnapDesigner.cs) (VB: [SnapDesigner.vb](./VB/SnapModule/Editors/SnapDesigner.vb))
* [InplaceDocumentCacheHelper.cs](./CS/SnapModule/Helpers/InplaceDocumentCacheHelper.cs) (VB: [InplaceDocumentCacheHelper.vb](./VB/SnapModule/Helpers/InplaceDocumentCacheHelper.vb))
* **[SnapDocumentHelper.cs](./CS/SnapModule/Helpers/SnapDocumentHelper.cs) (VB: [SnapDocumentHelper.vb](./VB/SnapModule/Helpers/SnapDocumentHelper.vb))**
<!-- default file list end -->
# How to integrate Snap reports in XAF WinForms


<p><strong>Scenario:<br /></strong><br />This example demonstrates one possible way of integrating <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument11373">Snap reports</a> in an XAF WinForms application. It contains the Snap module (the SnapModule class library) implementing this functionality and the SnapExampleXpo and SnapExampleEF projects demonstrating how to use this module with XPO and EF.</p>
<p>The example integration shows how to store reports layout in business objects and show a list of these objects, as this is done in the built-in <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3591">Reports module</a>. The main tool for working with these reports is a custom Snap designer form. Underlying business objects can be configured using custom buttons from this form. It is also possible to show a read-only preview form using corresponding actions.</p>
<p><br /><img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-integrate-snap-reports-in-xaf-winforms-t164798/15.1.9+/media/cd883611-bdb1-11e4-80ba-00155d624807.png"><br />Note that the Snap module from this example is not a part of the standard XAF delivery and is not tested as intensively as our built-in modules. Feel free to modify and test it further according to your business requirements.<br />If your clients require other scenarios of integrating the Snap reports, please describe them here or in the <a href="https://www.devexpress.com/Support/Center/p/Q503664">Create a Snap Report Module for XAF</a> ticket. Thank you in advance!<br /><br /><br /><strong>Steps to implement:<br /></strong><br />The Snap module contains a custom SnapDesigner form and classes used to integrate this form in an XAF application - business classes for storing reports and controllers used to show the designer and preview forms. To learn more about integrating custom forms and controls, refer to the <a href="https://documentation.devexpress.com/#Xaf/CustomDocument3610">Using a Custom Control that is not Integrated by Default</a> topic.</p>
<p>To see how the module works, download the example and launch the SnapExample.Win or SnapExampleEF.Win project. To create a new Snap document, navigate to the Reports navigation item and click New. A Snap designer will be opened (see the screenshot above). It contains several custom commands implemented especially for XAF:</p>
<p><br />  <strong>New</strong> - creates a new Snap document;<br />  <strong>Save </strong>- saves changes to the database. If the report is new, shows a DetailView allowing to define document's properties;<br />  <strong>Add XAF Data Source</strong> - shows a DetailView allowing to define a new data source. Adds this data source to the current Snap document;<br />  <strong>Edit Object</strong> - opens a DetailView of the current IDocumentData object that stores report's settings in the database. Use this button to remove or change the existing XAF data sources or other report's properties.<br /><br />To see information about built-in Snap designer's commands and learn more about Snap reports, refer to Snap <a href="https://documentation.devexpress.com/#WindowsForms/CustomDocument11373">documentation</a>.<br /><br /></p>
<p>To use the Snap module from this example in your application, perform the following steps:<br /><br />1. Download the example and build it with your version of our components.<br />2. Copy the DocumentData and DocumentDataSource classes from the example to your application's business classes module (<em>YourSolutionName.Module</em>). If you are using EF, copy classes from the SnapExampleEF\BusinessObjects folder. If you are using XPO, copy classes from the SnapExample.Module\BusinessObjects folder.<br />3. Add a reference to the SnapModule assembly to your project (<em>YourSolutionName.Win</em>).<br />4. Add SnapModule to your application's Modules list. To do this, either open the WinApplication.cs file of the <em>YourSolutionName.Win</em> project and add a SnapModule instance to the Modules collection in a constructor, or add the SnapModule project to your VS solution and drag & drop the Snap module from a toolbox in the Application Designer.<br />5. Set SnapModule's DocumentDataType property to the DocumentData type you have created in step 2.</p>

<br/>


