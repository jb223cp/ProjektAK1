﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebshopClick.Model;
using WebshopClick.Model.BLL;

namespace WebshopClick.Pages.WebAdminPages
{
    public partial class AdminProducts : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        public IEnumerable<Product> ProductListView_GetData(int maximumRows, int startRowIndex, out int totalRowCount)
        {
            try
            {
                if (CategoryDropDownList.SelectedValue != "-- Välj kategori --")
                {
                    int fil = Int32.Parse(CategoryDropDownList.SelectedValue);
                    return Service.GetProductPageWiseByCatID(fil, maximumRows, startRowIndex, out totalRowCount);
                }

                return Service.GetProductPageWise(maximumRows, startRowIndex, out totalRowCount);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då produktuppgifter skulle hämtas.");
                totalRowCount = 0;
                return null;
            }
        }

        protected void SelectionHasChanged(Object sender, System.EventArgs e)
        {
            productList.DataBind();
        }
        public IEnumerable<Category> CategoryDropDownList_GetData()
        {
            try
            {
                return Service.GetCategory();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kategoriuppgifter skulle hämtas.");
                return null;
            }
        }

        public void ProductListView_DeleteItem(int ProductID)
        {
            try
            {
                Service.DeleteProduct(ProductID);
                Response.RedirectToRoute("AProducts");
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då produktuppgiften skulle tas bort.");
            }
        }
        protected void ProductFormView_PageIndexChanging(object sender, FormViewPageEventArgs e)
        {

        }
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("AddProduct");
        }
    }
}