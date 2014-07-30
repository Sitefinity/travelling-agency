<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.DynamicModules.Web.UI.Frontend" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend" TagPrefix="sf" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Comments" %>
<sf:SitefinityLabel id="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" CssClass="sfitemFieldLbl" />
<sf:DynamicDetailContainer id="detailContainer" runat="server">
    <LayoutTemplate>        
        <div class="sfitemDetails sfdetails" data-sf-provider='<%# Eval("Provider.Name")%>' data-sf-id='<%# Eval("Id")%>' data-sf-type="Telerik.Sitefinity.DynamicModules.Model.DynamicContent">
            <div style="text-align:center">
                <sf:SitefinityLabel ID="mainShortTextFieldLiteral" runat="server" Text='<%# Eval("Name") %>' WrapperTagName="h1" HideIfNoText="true" CssClass="sfitemTitle sftitle" data-sf-field="Name" data-sf-ftype="ShortText" />
            </div>
            <sf:SitefinityLabel ID="SitefinityLabel1" runat="server" Text='<%# Eval("History")%>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemRichText" data-sf-field="History" data-sf-ftype="LongText"/>
            <div class='sfitemAddressWrp'>
                <sf:AddressField ID="AddressField1" runat="server" DataFieldName="Location" IsMapExpanded="true" AddressTemplate="#=Street# #=City# #=State# #=Country#" />
            </div>
        </div>
    </LayoutTemplate>
</sf:DynamicDetailContainer>
<asp:PlaceHolder ID="socialOptionsContainer" runat="server"></asp:PlaceHolder>