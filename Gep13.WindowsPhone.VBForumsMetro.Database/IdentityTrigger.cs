namespace Gep13.WindowsPhone.VBForumsMetro.Database
{
    using System.Linq;

    using Gep13.WindowsPhone.VBForumsMetro.Models;

    using Wintellect.Sterling;
    using Wintellect.Sterling.Database;

    public class IdentityTrigger<T> : BaseSterlingTrigger<T, int> where T: class, IBaseModel, new()
    {
        private static int idx = 1;

        public IdentityTrigger(ISterlingDatabaseInstance database)
        {
            if (database.Query<T, int>().Any())
            {
                idx = database.Query<T, int>().Max(key => key.Key) + 1;
            }
        }

        public override bool BeforeSave(T instance)
        {
            if (instance.Id < 1)
            {
                instance.Id = idx++;
            }

            return true;
        }

        public override void AfterSave(T instance)
        {
            return;
        }

        public override bool BeforeDelete(int key)
        {
            return true;
        }
    }
}