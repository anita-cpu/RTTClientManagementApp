using RTTClientManagementApp.Models;
using System;

namespace RTTClientManagementApp.Services
{
    public class ClientManager
    {

        /// <summary>
/// Validates the client data to ensure all required fields are populated.
/// </summary>
/// <param name="client">The client to validate.</param>
/// <exception cref="ArgumentException">Thrown when validation fails.</exception>
        public void ValidateClient(ClientModel client)
        {
            if (string.IsNullOrWhiteSpace(client.Name))
                throw new ArgumentException("Client name is required.");

            if (string.IsNullOrWhiteSpace(client.Gender))
                throw new ArgumentException("Gender is required.");

            foreach (var address in client.Addresses)
            {
                if (string.IsNullOrWhiteSpace(address.Type) || string.IsNullOrWhiteSpace(address.Street))
                    throw new ArgumentException("Address type and street are required.");
            }

            foreach (var contact in client.Contacts)
            {
                if (string.IsNullOrWhiteSpace(contact.Type) || string.IsNullOrWhiteSpace(contact.Value))
                    throw new ArgumentException("Contact type and value are required.");
            }
        }
    }
}