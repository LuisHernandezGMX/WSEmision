using System.Web;
using System.Web.Optimization;

namespace WSEmision
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /************************************* GENERAL *************************************/
            bundles.Add(new ScriptBundle("~/bundles/General").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js",
                "~/Scripts/fontawesome/all.js"
            ));

            bundles.Add(new StyleBundle("~/Content/General").Include(
                "~/Content/fontawesome-all.css",
                "~/Content/bootstrap.css",
                "~/Content/site.css"
            ));
            /************************************* GENERAL *************************************/
        }
    }
}
