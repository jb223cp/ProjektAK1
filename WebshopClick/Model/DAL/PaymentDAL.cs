﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebshopClick.Model.BLL;

namespace WebshopClick.Model.DAL
{
    public class PaymentDAL:DALBase
    {
        /// <summary>
        /// Method that returns all payment types
        /// </summary>
        /// <returns>List of payment objects</returns>
        public IEnumerable<Payment> GetPayment()
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    var payment = new List<Payment>(10);

                    var cmd = new SqlCommand("appSchema.uspGetPayment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        var PaymentIDIndex = reader.GetOrdinal("PaymentID");
                        int PaymentTypeIndex = reader.GetOrdinal("PaymentType");

                        while (reader.Read())
                        {
                            payment.Add(new Payment
                            {
                                PaymentID = reader.GetInt32(PaymentIDIndex),
                                PaymentType = reader.GetString(PaymentTypeIndex),
                            });
                        }
                    }
                    payment.TrimExcess();
                    return payment;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting payment types from the database.");
                }
            }
        }
        /// <summary>
        /// Updates payment
        /// </summary>
        /// <param name="payment"></param>
        public void UpdatePayment(Payment payment)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.uspUpdatePayment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.Parameters.Add("@PaymentID", SqlDbType.Int, 4).Value = payment.PaymentID;
                    cmd.Parameters.Add("@PaymentType", SqlDbType.NVarChar, 30).Value = payment.PaymentType;
                    cmd.ExecuteNonQuery();

                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void InsertPayment(Payment payment)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.uspInsertPayment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@PaymentType", SqlDbType.NVarChar, 30).Value = payment.PaymentType;
                    cmd.Parameters.Add("@PaymentID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    payment.PaymentID = (int)cmd.Parameters["@PaymentID"].Value;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        /// <summary>
        /// Method to get payment types by specific id
        /// </summary>
        /// <param name="id">Payment indetity</param>
        /// <returns>Payment</returns>
        public Payment GetPaymentById(int id)
        {
            using (SqlConnection conn = CreateConnection())
            {
                try
                {

                    SqlCommand cmd = new SqlCommand("appSchema.uspGetPaymentById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaymentID", id);
                    conn.Open();


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int PaymentIDIndex = reader.GetOrdinal("PaymentID");
                            int PaymentTypeIndex = reader.GetOrdinal("PaymentType");


                            return new Payment
                            {
                                PaymentID = reader.GetInt32(PaymentIDIndex),
                                PaymentType = reader.GetString(PaymentTypeIndex),
                            };
                        }
                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void DeletePayment(int ID)
        {

            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("appSchema.uspDeletePayment", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    cmd.Parameters.Add("@PaymentID", SqlDbType.Int, 4).Value = ID;
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}