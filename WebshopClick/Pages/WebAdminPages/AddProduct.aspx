﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/WebAdmin.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="WebshopClick.Pages.WebAdminPages.AddProduct" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="admin_content" runat="server">
    <asp:ValidationSummary
         runat="server" 
        CssClass="validation-summary-errors"
        ValidationGroup="ProductUpload" ID="Validation" />
    <asp:FormView ID="ProductFormView" runat="server"
        ItemType="WebshopClick.Model.BLL.Product"
        DefaultMode="Insert"
        RenderOuterTable="false"
        InsertMethod="ProductFormView_InsertItem" >
        <InsertItemTemplate>
            <div class="editor-label">
                <label for="Name">Namn</label>
            </div>
            <div class="editor-field">
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ett namn måste anges." ControlToValidate="Name" ValidationGroup="ProductUpload"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Name" runat="server" Text='<%# BindItem.Name %>' MaxLength="40" />
            </div>
            <div class="editor-label">
                <label for="Priset">Priset</label>
            </div>
            <div class="editor-field">
                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" runat="server" ErrorMessage="Priset är inte i rätt format." ControlToValidate="Priset" ValidationGroup="ProductUpload" ValidationExpression='"^\d ValidationExpression="^\d+(.\d{1,2})?$" +(.\d{1,2})?$"'></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator4" runat="server" ErrorMessage="Ett namn måste anges." ControlToValidate="Priset" ValidationGroup="ProductUpload"></asp:RequiredFieldValidator>
                <asp:TextBox ID="Priset" runat="server" Text='<%# BindItem.Price %>' />
            </div>
            <div class="editor-label">
                <label for="Description">Beskrivning</label>
            </div>
            <div class="editor-field">
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator2" runat="server" ErrorMessage="En beskrivning måste anges." ControlToValidate="Description" ValidationGroup="ProductUpload"></asp:RequiredFieldValidator>
                <asp:TextBox TextMode="MultiLine" Rows="5" ID="Description" runat="server" Text='<%# BindItem.Description %>' MaxLength="200" />
            </div>
            <div class="editor-label">
                <label for="CategoryDropDownList">Kategori</label>
            </div>
            <div class="editor-field">
                <asp:DropDownList ID="CategoryDropDownList" runat="server"
                                    ItemType="WebshopClick.Model.BLL.Category"
                                    SelectMethod="CategoryDropDownList_GetData"
                                    DataTextField="CategoryName"
                                    DataValueField="CategoryID"
                                    SelectedValue='<%# BindItem.CategoryID %>'
                                    Enabled="true" />

                
            </div>
            <div class="editor-field">
                <div class="editor-label">
                    <label for="AddImage">Bild</label>
                </div>
                <asp:RequiredFieldValidator Display="None" ID="RequiredFieldValidator3" runat="server" ErrorMessage="Ett bildnamn måste anges." ControlToValidate="AddImage" ValidationGroup="ProductUpload"></asp:RequiredFieldValidator>
                <asp:TextBox ID="AddImage" runat="server" Text='<%# BindItem.Image %>' Enabled="false" MaxLength="50" />
            </div>




            <div>
                <asp:LinkButton runat="server" Text="Spara" CommandName="Insert" />
            </div>
        </InsertItemTemplate>
        
    </asp:FormView>
    <%-- Uploading of images --%>
            <asp:Panel ID="UploadPanel" runat="server"> 
      
            <asp:ValidationSummary 
                ID="ValidationSummary1" 
                ValidationGroup="ImageUpload"
                runat="server" />  
            <asp:FileUpload ID="FileUpload" runat="server" />
       
             <asp:RequiredFieldValidator 
                ID="RequiredFieldValidator1" 
                runat="server" 
                ErrorMessage="En fil måste finns" 
                ControlToValidate="FileUpload" 
                Display="None"
                ValidationGroup="ImageUpload">
            </asp:RequiredFieldValidator>
       
            <asp:RegularExpressionValidator 
                ID="RegularExpressionValidator1" 
                runat="server" 
                ErrorMessage="Endast bilder av typerna gif, jpeg eller png är tillåtna." 
                ControlToValidate="FileUpload" 
                ValidationExpression='.*(\.[Jj][Pp][Gg]|\.[Gg][Ii][Ff]|\.[Jj][Pp][Ee][Gg]|\.[Pp][Nn][Gg])' 
                Display="None" 
                ValidationGroup="ImageUpload"></asp:RegularExpressionValidator>
            <asp:Button ID="UploadButton" runat="server" Text="Ladda upp" OnClick="UploadButton_Click" />
        </asp:Panel>
<%-- Uploading of images END --%>
</asp:Content>
