using System;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Web.UI;

namespace SitefinityWebApp.Custom_widgets
{
    /// <summary>
    /// Custom dynamic content view displaying how items can be filtered through the API
    /// </summary>
    public class CountryHotelsView : DynamicContentView
    {
        protected override void InitializeMasterView()
        {
            //if (this.HideListViewOnChildDetailsView && this.CanResolveChildItemFromUrl())
            //{
            //    return;
            //}

            //if (this.FilterByParentUrl)
            //{
            //    Guid parentItemId;
            //    if (this.CanResolveParentItemFromUrl(out parentItemId))
            //    {
            //        this.SetupMasterViewControl();
            //        this.MasterViewControl.MasterViewDefinition.ItemsParentsIds = new Guid[] { parentItemId };
            //    }
            //}
            //else
            //{
            //    this.SetupMasterViewControl();
            //}
        }
    }
}
