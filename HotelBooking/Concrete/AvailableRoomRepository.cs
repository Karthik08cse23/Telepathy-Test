﻿using HotelBooking.Models;
using HotelBooking.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HotelBooking.Concrete
{
    public class AvailableRoomRepository : IAvailableRoomRepository
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        public List<AvailableRooms> GetAvailableRooms()
        {
            List<AvailableRooms> roomsList = new List<AvailableRooms>();
            DataTable dt = new DataTable();
            string strcon = ConfigurationManager.ConnectionStrings["AvailableRooms"].ConnectionString;
            using (conn = new SqlConnection(strcon))
            {
                cmd = new SqlCommand("dbo.GetAvailableRooms", conn);
                using (cmd)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AvailableRooms rooms = new AvailableRooms();
                        rooms.roomNo = dt.Rows[i]["RoomNo"].ToString();
                        rooms.roomStatus = ((HotelEnum.Status)(Convert.ToInt32(dt.Rows[i]["Status"])));
                        rooms.priority = Convert.ToInt32(dt.Rows[i]["PriorityOrder"]);
                        roomsList.Add(rooms);
                    }
                    //using (reader)
                    //{
                    //    while (reader.Read())
                    //    {
                    //        AvailableRooms rooms = new AvailableRooms();

                    //        rooms.roomNo = Convert.ToInt32(reader["RoomNo"]);
                    //        rooms.roomStatus = ((HotelEnum.Status)(Convert.ToInt32(reader["Status"])));
                    //        roomsList.Add(rooms);
                    //    }
                    //    reader.Close();
                    //}
                }
                conn.Close();
            }
            return roomsList;
        }

        public void HouseKeeping(HouseKeeping houseKeeping)
        {
            string strcon = ConfigurationManager.ConnectionStrings["AvailableRooms"].ConnectionString;
            using (conn = new SqlConnection(strcon))
            {
                cmd = new SqlCommand("dbo.SPHouseKeeping", conn);

                using (cmd)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@roomNo", SqlDbType.NVarChar).Value = houseKeeping.roomNo;
                    cmd.Parameters.Add("@status", SqlDbType.Int).Value = houseKeeping.Status;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public void CheckOut(string roomNo)
        {
            string strcon = ConfigurationManager.ConnectionStrings["AvailableRooms"].ConnectionString;
            using (conn = new SqlConnection(strcon))
            {
                cmd = new SqlCommand("dbo.SPCheckOutRooms", conn);

                using (cmd)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("@roomNo", SqlDbType.NVarChar).Value = roomNo;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public List<string> BookRooms(int count)
        {
            List<string> roomsBooked = new List<string>();
            DataTable dt = new DataTable();
            string strcon = ConfigurationManager.ConnectionStrings["AvailableRooms"].ConnectionString;
            using (conn = new SqlConnection(strcon))
            {
                cmd = new SqlCommand("dbo.SPBookRooms", conn);
                cmd.Parameters.Add("@count", SqlDbType.Int).Value = count;
                using (cmd)
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string RoomNo = dt.Rows[i]["RoomNo"].ToString();
                        roomsBooked.Add(RoomNo);
                    }

                }
                conn.Close();
            }
            return roomsBooked;
        }
    }
}