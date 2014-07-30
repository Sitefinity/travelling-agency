<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.DynamicModules.Web.UI.Frontend" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend" TagPrefix="sf" %>      
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Libraries.Web.UI" TagPrefix="sf" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Comments" %>
<sf:SitefinityLabel id="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" CssClass="sfitemFieldLbl" />
<sf:DynamicDetailContainer id="detailContainer" runat="server">
    <LayoutTemplate>        
        <div class="sfitemDetails sfdetails" data-sf-provider='<%# Eval("Provider.Name")%>' data-sf-id='<%# Eval("Id")%>' data-sf-type="Telerik.Sitefinity.DynamicModules.Model.DynamicContent">
            <div style="text-align:center">
                <sf:SitefinityLabel ID="mainShortTextFieldLiteral" runat="server" Text='<%# Eval("Name") %>' WrapperTagName="h1" HideIfNoText="true" 
                    CssClass="sfitemTitle sftitle" data-sf-field="Name" data-sf-ftype="ShortText" />
                <sf:SitefinityLabel ID="SitefinityLabel9" runat="server" Text='<%# Eval("Rating")%>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemNumber" />
                <sf:SitefinityLabel ID="SitefinityLabel8" runat="server" Text= 'stars' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />     
            </div>
            <sf:SitefinityLabel ID="SitefinityLabel7" runat="server" Text='<%# Eval("Overview")%>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemRichText" data-sf-field="Overview" data-sf-ftype="LongText"/>

            <div class='sfitemAddressWrp'>        
                 
                <sf:AddressField ID="AddressField1" runat="server" DataFieldName="Location" IsMapExpanded="false" AddressTemplate="#=Street# #=City# #=State# #=Country#" />
            </div>

            <div class="sfSnglRelatedItmWrp">
                <img src='<%# Eval("MainPicture.MediaUrl")%>' alt='<%# Eval("MainPicture.AlternativeText")%>' title='<%# Eval("MainPicture.Title") %>' />
            </div>

            <sf:DynamicChoiceField ID="DynamicChoiceField1" runat="server" DisplayMode="Read" RenderChoicesAs="CheckBoxes" DataItemType="Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Hotel" DataFieldName="FoodAndDrink" Title="Select Food & Drink:" Value='<%# Eval("FoodAndDrink") %>' CssClass="sfitemChoices" DisableClientScripts="true" data-sf-field="FoodAndDrink" data-sf-ftype="ChoicesCheckBoxes">
            </sf:DynamicChoiceField>
            <sf:DynamicChoiceField ID="DynamicChoiceField2" runat="server" DisplayMode="Read" RenderChoicesAs="CheckBoxes" DataItemType="Telerik.Sitefinity.DynamicTypes.Model.TravellingAgency.Hotel" DataFieldName="Activities" Title="Activities:" Value='<%# Eval("Activities") %>' CssClass="sfitemChoices" DisableClientScripts="true" data-sf-field="Activities" data-sf-ftype="ChoicesCheckBoxes">
            </sf:DynamicChoiceField>
            <div class='sfitemShortTxtWrp'>        
                <sf:SitefinityLabel ID="SitefinityLabel1" runat="server" Text='Checkin:' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />        
                <sf:SitefinityLabel ID="SitefinityLabel2" runat="server" Text='<%# Eval("Checkin")%>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemShortTxt" data-sf-field="Checkin" data-sf-ftype="ShortText"/>
            </div>
            <div class='sfitemShortTxtWrp'>        
                <sf:SitefinityLabel ID="SitefinityLabel3" runat="server" Text='Checkout:' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemFieldLbl" />        
                <sf:SitefinityLabel ID="SitefinityLabel4" runat="server" Text='<%# Eval("Checkout")%>' WrapperTagName="div" HideIfNoText="true" CssClass="sfitemShortTxt" data-sf-field="Checkout" data-sf-ftype="ShortText"/>
            </div>
            
        </div>
    </LayoutTemplate>
</sf:DynamicDetailContainer>
<asp:PlaceHolder ID="socialOptionsContainer" runat="server"></asp:PlaceHolder>
<sf:CommentsWidget runat="server" ID="commentsWidget" />