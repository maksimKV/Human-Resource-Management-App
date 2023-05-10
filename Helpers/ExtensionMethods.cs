using Human_Resource_Management_App.Entities;

namespace Human_Resource_Management_App.Helpers
{
    public static class ExtensionMethods
    {
        public static Admin WithoutPassword(this Admin admin)
        {
            admin.Password = null;
            return admin;
        }
    }
}
