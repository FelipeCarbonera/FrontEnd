using System.Web;
using System.Web.Optimization;

namespace Frontend
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/popper").Include(
                        "~/Scripts/umd/popper.js"));

            // adicionado o 'methods_pt.js' para localizar o validate em pt-br-
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/methods_pt.js"));

            // carrega os scripts do MoneyMask, para manipulação de moedas
            bundles.Add(new ScriptBundle("~/bundles/jquery.moneymask").Include(
                        "~/Scripts/jquery.moneymask*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
