﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarRentalLogicServer.Models;
using HotChocolate;
using IHttpClientFactory = CarRentalLogicServer.APIConsumer.ClientFactory.IHttpClientFactory;

namespace CarRentalLogicServer.APIConsumer
{
    //todo check the methods, it was copied ctrlR
    public class ReservationWebService : IReservationService
    {
        private string uri = "http://localhost:8080/api";

        private readonly HttpClient client;

        public ReservationWebService([Service] IHttpClientFactory clientFactory)
        {
            client = clientFactory.GetHttpClient();
        }
        
        
        public async Task<List<Reservation>> GetReservationsAsync()
        {
            HttpResponseMessage response = await client.GetAsync(uri + "/reservations");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            string message = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Reservation>>(message);
            // return message;
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            var message = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(message);
            // return message;
        }


        // method not using json to transfer
        public async Task<Reservation> CreateReservationAsync(Reservation reservation)
        {
            string reservationAsJson = JsonSerializer.Serialize(reservation);
            HttpContent content = new StringContent(reservationAsJson,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response = await client.PostAsync(uri + "/reservations", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.ReasonPhrase);

            string message = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(message);
        }

        // method using JSON to transfer data
        /*public async Task<string> CreateReservationAsync(string reservation)
        {
            HttpContent content = new StringContent(reservation,
                Encoding.UTF8,
                "application/json");
            HttpResponseMessage response = await client.PostAsync(uri + "/reservations", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            string message = await response.Content.ReadAsStringAsync();
            return message;
        }*/

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
        {
            string reservationAsJson = JsonSerializer.Serialize(reservation);
            HttpContent content = new StringContent(reservationAsJson,
                Encoding.UTF8,
                "application/json");
            
            HttpResponseMessage response = await client.PutAsync($"{uri}/reservations/{reservation.Id}", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            string message = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Reservation>(message);
        }

        public async Task<bool> DeleteReservationAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{uri}/reservations/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.ReasonPhrase);
            }

            var isDeletedMap =
                JsonSerializer.Deserialize<Dictionary<string, bool>>(response.Content.ReadAsStringAsync().Result);
            if (isDeletedMap == null)
            {
                return false;
            }

            return isDeletedMap["deleted"];
        }
    }
}