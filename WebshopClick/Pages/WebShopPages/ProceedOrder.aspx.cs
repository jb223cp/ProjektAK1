﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebshopClick.Model.BLL;
using WebshopClick.Model.Code;

namespace WebshopClick.Pages.WebShopPages
{
    public partial class ProceedOrder : System.Web.UI.Page
    {
        private Service _service;
        private Service Service
        {

            get { return _service ?? (_service = new Service()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HiddenField hiddenStatus = OrderFormView.FindControl("HiddenFieldStatus") as HiddenField;
                HiddenField hiddenUser = OrderFormView.FindControl("HiddenFieldUser") as HiddenField;
                Label labelDate = OrderFormView.FindControl("LabelDate") as Label;
                UpdateCart();
                isLoged();
                if (hiddenStatus != null)
                {
                    hiddenStatus.Value = "1";
                }
                if (hiddenUser != null)
                {
                    hiddenUser.Value = "1";
                }

                if (labelDate != null)
                {
                    labelDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
            if (Session["Success"] as bool? == true)
            {
                FlashPlaceHolder.Visible = true;
            }
            if (Session["Fail"] as bool? == true)
            {
                PlaceHolder1.Visible = true;
            }
        }
        protected void UpdateCart()
        {
            int cartTotalQuantity = 0;
            List<Item> cart = (List<Item>)Session["cart"];
            if ((Session["cart"] != null))
            {
                if ((cart.Count() != 0))
                {

                    foreach (Item item in cart)
                    {
                        cartTotalQuantity = cartTotalQuantity + item.Quantity;
                    }
                    lblCart.Text = (cartTotalQuantity).ToString();
                }
                else
                {
                    lblCart.Text = "0";
                }
            }
            return;
        }
        protected void Close_Click(object sender, EventArgs e)
        {
            Session["Success"] = false;
            FlashPlaceHolder.Visible = false;
        }
        protected void Close1_Click(object sender, EventArgs e)
        {
            Session["Fail"] = false;
            PlaceHolder1.Visible = false;
        }
        protected void btnCart_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("ViewCart");
        }
        protected void btnPage_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("MyPages");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];

            if (user != null)
            {
                HiddenField hiddenUser = OrderFormView.FindControl("HiddenFieldUser") as HiddenField;
                TextBox name = OrderFormView.FindControl("Name") as TextBox;
                TextBox address = OrderFormView.FindControl("Address") as TextBox;
                TextBox postnumber = OrderFormView.FindControl("Postnumber") as TextBox;
                TextBox city = OrderFormView.FindControl("TextBox1") as TextBox;
                hiddenUser.Value = (user.UserID).ToString();
                name.Text = user.Name;
                address.Text = user.Address;
                postnumber.Text = user.Postnumber;
                city.Text = user.City;
            }

        }
        protected void btnHem_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("index");
        }
        public IEnumerable<Payment> PaymentDropDownList_GetData()
        {
            try
            {
                return Service.GetPayment();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då bästalningsättsuppgifter skulle hämtas.");
                return null;
            }
        }
        void OrderFormView_DataBound(Object sender, EventArgs e)
        {
            HiddenField hiddenStatus = OrderFormView.FindControl("HiddenFieldStatus") as HiddenField;
            HiddenField hiddenUser = OrderFormView.FindControl("HiddenFieldUser") as HiddenField;
            Label labelDate = OrderFormView.FindControl("LabelDate") as Label;

            if (hiddenStatus == null)
            {
                hiddenStatus.Value = "1";
            }
            if (hiddenUser == null)
            {
                hiddenUser.Value = "1";
            }

            if (labelDate == null)
            {
                labelDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            }
        }
        protected void OrderFormView_ItemCreated(object sender, EventArgs e)
        {
            if (OrderFormView.CurrentMode == FormViewMode.Insert)
            {
                HiddenField hiddenStatus = OrderFormView.FindControl("HiddenFieldStatus") as HiddenField;
                HiddenField hiddenUser = OrderFormView.FindControl("HiddenFieldUser") as HiddenField;
                Label labelDate = OrderFormView.FindControl("LabelDate") as Label;

                if (hiddenStatus == null)
                {
                    hiddenStatus.Value = "1";
                }
                if (hiddenUser == null)
                {
                    hiddenUser.Value = "1";
                }

                if (labelDate == null)
                {
                    labelDate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                }
            }
        }
        protected void ButtonAdmin_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("alogin");
        }

        public void OrderFormView_InsertItem(WebshopClick.Model.BLL.Order order)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Service service = new Service();
                    Orderrow orderrow = new Orderrow();
                    List<Item> cart = (List<Item>)Session["cart"];

                    if (cart.Count > 0)
                    {
                        service.UpdateOrder(order);

                        foreach (Item item in cart)
                        {
                            orderrow.OrderID = order.OrderID;
                            orderrow.ProductID = item.Product.ProductID;
                            orderrow.TaxID = 1;
                            orderrow.Quantity = item.Quantity;
                            orderrow.Price = item.Product.Price;
                            orderrow.Discount = Convert.ToDecimal(0);
                            service.UpdateOrderrow(orderrow);
                            orderrow.RowID = 0;
                        }
                        cart.Clear();
                        FlashPlaceHolder.Visible = true;
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade då beställning skulle genomföras.");
                }
            }
        }
        protected void isLoged()
        {
            WebshopClick.Model.BLL.User user = (WebshopClick.Model.BLL.User)Session["User"];
            ButtonAdmin.Visible = false;

            if (user == null)
            {

                btnLogin.Text = "Logga in";

                return;
            }
            else
            {
                if (user.Administrator == true)
                {
                    ButtonAdmin.Visible = true;
                }
                btnLogin.Text = "Hej " + user.LoginID;

                if (user.Administrator == true)
                {
                    ButtonAdmin.Visible = true;
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Profile");
        }
        protected void ImgSearch_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("index");
        }
    }
}