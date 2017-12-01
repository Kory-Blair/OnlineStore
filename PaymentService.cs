using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Braintree;

namespace OnlineStore
{
    public class PaymentService
    {
        protected Braintree.BraintreeGateway gateway;
        public PaymentService()
        {
            string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
            string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
            string publickey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privatekey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            gateway = new Braintree.BraintreeGateway(environment, merchantId, publickey, privatekey);

        }

        public Braintree.Customer GetCustomer(string email)
        {
            var customerGateway = gateway.Customer;
            Braintree.CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
            query.Email.Is(email);
            var matchedCustomers = customerGateway.Search(query);
            Braintree.Customer customer = null;
            if (matchedCustomers.Ids.Count == 0)
            {
                Braintree.CustomerRequest newCustomer = new Braintree.CustomerRequest();
                newCustomer.Email = email;

                var result = customerGateway.Create(newCustomer);
                customer = result.Target;
            }
            else
            {
                customer = matchedCustomers.FirstItem;
            }
            return customer;
        }

        internal Customer UpdateCustomer(string firstName, string lastName, string id)
        {
            Braintree.CustomerRequest request = new Braintree.CustomerRequest();
            request.FirstName = firstName;
            request.LastName = lastName;
            var result = gateway.Customer.Update(id, request);
            return result.Target;
        }

        internal void DeleteAddress(string email, string id)
        {
            Customer c = GetCustomer(email);
            gateway.Address.Delete(c.Id, id);
        }

        public void AddAddress(string email, string firstName, string lastName, string company, string streetAddress, string extendedAddress, string locality, string region, string postcalCode, string countryName)
        {
            Customer c = GetCustomer(email);

            Braintree.AddressRequest newAddress = new Braintree.AddressRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Company = company,
                CountryName = countryName,
                PostalCode = postcalCode,
                ExtendedAddress = extendedAddress,
                Locality = locality,
                Region = region,
                StreetAddress = streetAddress
            };
            gateway.Address.Create(c.Id, newAddress);
        }
        public string AuthorizeCard(string email, decimal total, decimal tax, string trackingNumber, string addressId, string cardholderName, string cvv, string cardNumber, string expirationMonth, string expirationYear)
        {
            var customer = GetCustomer(email);
            Braintree.TransactionRequest transaction = new Braintree.TransactionRequest();
            transaction.Amount = total;
            transaction.TaxAmount = tax;
            transaction.OrderId = trackingNumber;
            transaction.CustomerId = customer.Id;
            transaction.ShippingAddressId = addressId;
            transaction.CreditCard = new Braintree.TransactionCreditCardRequest
            {
                CardholderName = cardholderName,
                CVV = cvv,
                Number = cardNumber,
                ExpirationYear = expirationYear,
                ExpirationMonth = expirationMonth
            };
            var result = gateway.Transaction.Sale(transaction);

            return result.Message;
            

        }

        internal string AuthorizeCard(string contactEmail, decimal total, decimal? tax, string trackingNumber, string shippingAddress, string cardholderName, string cVV, string creditCardNumber, string expirationMonth, string expirationYear)
        {
            throw new NotImplementedException();
        }
    }
}