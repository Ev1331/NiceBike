using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nicebike.Models;
using Nicebike.Views;

namespace Nicebike.ViewModels
{
    internal class SearchBarFilter
    {
        public List<Customer> GetFilteredCustomers(string filterText)
        {
            CustomersManagement customersManagement = new CustomersManagement();
            List<Customer> customers = customersManagement.GetAllCustomers();

            List<Customer> filteredCustomers = customers.Where(x => !string.IsNullOrWhiteSpace(x.name) && x.name.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (filteredCustomers == null || filteredCustomers.Count <= 0)
            {
                filteredCustomers = customers.Where(x => !string.IsNullOrWhiteSpace(x.surname) && x.surname.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredCustomers;

            if (filteredCustomers == null || filteredCustomers.Count <= 0)
            {
                filteredCustomers = customers.Where(x => !string.IsNullOrWhiteSpace(x.mail) && x.mail.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredCustomers;

            if (filteredCustomers == null || filteredCustomers.Count <= 0)
            {
                filteredCustomers = customers.Where(x => !string.IsNullOrWhiteSpace(x.phone) && x.phone.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredCustomers;

            if (filteredCustomers == null || filteredCustomers.Count <= 0)
            {
                filteredCustomers = customers.Where(x => !string.IsNullOrWhiteSpace(x.town) && x.town.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredCustomers;

            return filteredCustomers;
        }

        public List<Part> GetFilteredParts(string filterText)
        {
            PartsManagement partsManagement = new PartsManagement();
            List<Part> parts = partsManagement.GetAllParts();

            List<Part> filteredParts = parts.Where(x => !string.IsNullOrWhiteSpace(x.reference) && x.reference.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (filteredParts == null || filteredParts.Count <= 0)
            {
                filteredParts = parts.Where(x => !string.IsNullOrWhiteSpace(x.description) && x.description.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredParts;

            if (filteredParts == null || filteredParts.Count <= 0)
            {
                filteredParts = parts.Where(x => !string.IsNullOrWhiteSpace(x.supplierName) && x.description.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredParts;
            return filteredParts;
        }

        public List<Supplier> GetFilteredSuppliers(string filterText)
        {
            SupplierManagement supplierManagement = new SupplierManagement();
            List<Supplier> suppliers = supplierManagement.GetAllSuppliers();

            List<Supplier> filteredSuppliers = suppliers.Where(x => !string.IsNullOrWhiteSpace(x.name) && x.name.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (filteredSuppliers == null || filteredSuppliers.Count <= 0)
            {
                filteredSuppliers = suppliers.Where(x => !string.IsNullOrWhiteSpace(x.mail) && x.mail.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredSuppliers;

            if (filteredSuppliers == null || filteredSuppliers.Count <= 0)
            {
                filteredSuppliers = suppliers.Where(x => !string.IsNullOrWhiteSpace(x.phone) && x.phone.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredSuppliers;

            if (filteredSuppliers == null || filteredSuppliers.Count <= 0)
            {
                filteredSuppliers = suppliers.Where(x => !string.IsNullOrWhiteSpace(x.town) && x.town.Contains(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else return filteredSuppliers;

            return filteredSuppliers;
        }
    }
}
