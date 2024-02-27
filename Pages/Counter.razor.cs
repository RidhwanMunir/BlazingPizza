using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using BlazingPizza.Data;
namespace BlazingPizza.Pages
{
    public partial class PizzaSales : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        // Inject the state service 
        [Inject]
        public PizzaSalesState SalesState { get; set; }

        // Define a method to increment the number of pizzas sold today

        /*private void IncrementSales()
        {
          SalesState.PizzasSoldToday++;
        } */

        // Define a method to increment the number of pizzas sold today
        // Define a method to increment the number of pizzas sold today (asynchronous)
        public async Task IncrementSalesAsync()
        {
            try
            {
                // Simulate an asynchronous operation
                await Task.Delay(10);

                // save to LocalStorage
                SalesState.IncrementSales();
                await LocalStorage.SetItemAsync("pizzasSoldToday", SalesState.PizzasSoldToday);

                // Replace with your actual asynchronous operation call
                // which may throw specific exceptions like:
                // - HttpRequestException (network errors)
                // - ArgumentException (invalid input data)
                // - Exception (any other unexpected error)

            }
            catch (HttpRequestException ex)
            {
                // Network error handling
                Console.WriteLine($"Network error: {ex.Message}");
                // Display user-friendly message:
                Console.WriteLine("An unexpected error occurred while processing your request. Please try again later.");
            }
            catch (ArgumentException ex)
            {
                // Invalid input data handling
                Console.WriteLine($"Invalid input data: {ex.Message}");
                // Display user-friendly message:
                Console.WriteLine("Please check your input and try again.");
            }
            catch (Exception ex)
            {
                // Any other unexpected error handling
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Log the error for debugging
                // ... (use logging library)
                // Display generic error message:
                Console.WriteLine("An unexpected error occurred. Please try again later.");
            }
            finally
            {
                // Optional: Code to run regardless of exceptions (e.g., UI updates)
                // Optional final actions (e.g., disable button while processing)
            }
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadDataFromLocalStorage();
        }

        private async Task LoadDataFromLocalStorage()
        {
            // Try to get the pizza count from local storage
            try
            {
                if (await LocalStorage.GetItemAsync<int>("pizzasSoldToday") is int savedPizzaCount)
                {
                    SalesState.PizzasSoldToday = savedPizzaCount;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine(ex.Message);
            }
        }
    }
}