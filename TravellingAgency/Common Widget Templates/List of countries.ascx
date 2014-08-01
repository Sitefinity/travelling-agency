<%@ Control Language="C#" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.Comments" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.Fields" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend" TagPrefix="sf" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Import Namespace="Telerik.Sitefinity.Model" %>
<%@ Import Namespace="Telerik.Sitefinity.DynamicModules.Model" %>

<sf:SitefinityLabel ID="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" CssClass="sfitemFieldLbl" />
<telerik:RadListView ID="dynamicContentListView" ItemPlaceholderID="ItemsContainer" runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <LayoutTemplate>
        <ul class="sfitemsList sfitemsListTitleDateTmb sflist">
            <asp:PlaceHolder ID="ItemsContainer" runat="server" />
        </ul>
    </LayoutTemplate>
    <ItemTemplate>
        <li class="sfitem sflistitem sfClearfix" data-sf-provider='<%# Eval("Provider.Name")%>' data-sf-id='<%# Eval("Id")%>' data-sf-type="Telerik.Sitefinity.DynamicModules.Model.DynamicContent">
            <div class="sfSnglRelatedItmWrp">
                <div class="sfrelatedItmWrp">
                    <img style='width: 300px; height: 200px' src='<%# Eval("MainPicture.MediaUrl")%>' alt='<%# Eval("MainPicture.AlternativeText")%>' title='<%# Eval("MainPicture.Title") %>' />
                </div>
            </div>
            <h2 class="sfitemTitle sftitle">
                <sf:DetailsViewHyperLink ID="DetailsViewHyperLink" TextDataField="Name" runat="server" data-sf-field="Name" data-sf-ftype="ShortText" />
            </h2>
            <div class="sfCitiesCount">
                <sf:SitefinityLabel ID="SitefinityLabel1" runat="server" Text='<%# ((DynamicContent)Container.DataItem).GetChildItemsCount("Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.City") %>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />      
                <label> cities</label>
            </div>
        </li>
    </ItemTemplate>
</telerik:RadListView>
<sf:Pager ID="pager" runat="server"></sf:Pager>
<asp:PlaceHolder ID="socialOptionsContainer" runat="server"></asp:PlaceHolder>