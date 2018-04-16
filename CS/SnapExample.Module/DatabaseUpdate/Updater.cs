using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;

namespace SnapExampleXPO.Module.DatabaseUpdate {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) :
            base(objectSpace, currentDBVersion) {
        }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            CreatePerson("Mary", "Tellitson");
            CreatePerson("John", "Nilsen");
        }
        private void CreatePerson(string firstName, string lastName) {
            Person person = ObjectSpace.FindObject<Person>(CriteriaOperator.Parse("FirstName = ? and LastName = ?", firstName, lastName));
            if (person == null) {
                person = ObjectSpace.CreateObject<Person>();
                person.FirstName = firstName;
                person.LastName = lastName;
            }
        }
    }
}
