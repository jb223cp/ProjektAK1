﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/WebAdmin.Master" AutoEventWireup="true" CodeBehind="AdminProducts.aspx.cs" Inherits="WebshopClick.Pages.WebAdminPages.AdminProducts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="admin_content" runat="server">
    <h1>
        Ny produkt
    </h1>
    <asp:ValidationSummary runat="server" CssClass="validation-summary-errors" />
    <asp:FormView ID="ProductFormView" runat="server"
        ItemType="WebshopClick.Model.BLL.Products"
        DefaultMode="Insert"
        RenderOuterTable="false"
        InsertMethod="ProductFormView_InsertItem" OnPageIndexChanging="ProductFormView_PageIndexChanging">
        <InsertItemTemplate>
            <div class="editor-label">
                <label for="Name">Namn</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Name" runat="server" Text='<%# BindItem.Name %>' />
            </div>
            <div class="editor-label">
                <label for="Address">Priset</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="Priset" runat="server" Text='<%# BindItem.Price %>' />
            </div>
            <div class="editor-label">
                <label for="PostalCode">Beskrivning</label>
            </div>
            <div class="editor-field">
                <asp:TextBox ID="PostalCode" runat="server" Text='<%# BindItem.Description %>' />
            </div>
            <div class="editor-label">
                <label for="City">Kategori</label>
            </div>
            <div class="editor-field">
                <asp:DropDownList ID="CategoryDropDownList" runat="server"
                                    ItemType="WebShop.Model.BLL.Category"
                                    SelectMethod="CategoryDropDownList_GetData"
                                    DataTextField="Name"
                                    DataValueField="CategoryID"
                                    SelectedValue='<%# Item.CategoryID %>'
                                    Enabled="false" />
            </div>

<%-- Uploading of images --%>
            <asp:Panel ID="UploadPanel" runat="server"> 
      
            <asp:ValidationSummary 
                ID="ValidationSummary1" 
                runat="server" />  
            <asp:FileUpload ID="FileUpload" runat="server" />
       
            <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" 
                runat="server" 
                ErrorMessage="En fil måste väljas" 
                ControlToValidate="FileUpload" 
                Display="None">
            </asp:RequiredFieldValidator>
       
            <asp:RegularExpressionValidator 
                ID="RegularExpressionValidator1" 
                runat="server" 
                ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna." 
                ControlToValidate="FileUpload" 
                ValidationExpression='.*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])' 
                Display="None"></asp:RegularExpressionValidator>
            <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
        </asp:Panel>
<%-- Uploading of images END --%>


            <div>
                <asp:LinkButton runat="server" Text="Spara" CommandName="Insert" />
            </div>
        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
