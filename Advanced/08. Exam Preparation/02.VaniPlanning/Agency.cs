namespace _02.VaniPlanning
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Agency : IAgency
    {
        private Dictionary<string, Invoice> invoices = new Dictionary<string, Invoice>();

        public void Create(Invoice invoice)
        {
            if (invoices.ContainsKey(invoice.SerialNumber))
            {
                throw new ArgumentException();
            }

            invoices[invoice.SerialNumber] = invoice;
        }

        public void ThrowInvoice(string number)
        {
            if (!invoices.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            invoices.Remove(number);
        }

        public void ThrowPayed()
        {
            var toRemove = invoices.Values.Where(i => i.Subtotal == 0)
                .Select(i => i.SerialNumber).ToList();

            foreach (var number in toRemove)
            {
                invoices.Remove(number);
            }
        }

        public int Count()
        {
            return invoices.Count;
        }

        public bool Contains(string number)
        {
            return invoices.ContainsKey(number);
        }

        public void PayInvoice(DateTime due)
        {
            var result = invoices.Values.Where(i => i.DueDate == due).Select(i => i.Subtotal = 0);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }
        }

        public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
        {
            return invoices.Values.Where(i => start <= i.IssueDate && i.IssueDate <= end)
                .OrderBy(i => i.IssueDate)
                .ThenBy(i => i.DueDate);
        }

        public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
        {
            var keys = invoices.Keys.Where(k => k.Contains(serialNumber));

            if (keys.Count() == 0)
            {
                throw new ArgumentException();
            }

            return keys.OrderByDescending(k => k).Select(k => invoices[k]);
        }

        public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
        {
            var result = invoices.Values.Where(i => start < i.DueDate && i.DueDate < end);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in result)
            {
                invoices.Remove(invoice.SerialNumber);
            }

            return invoices.Values;
        }

        public IEnumerable<Invoice> GetAllFromDepartment(Department department)
        {
            return invoices.Values.Where(i => i.Department == department)
                .OrderByDescending(i => i.Subtotal)
                .ThenBy(i => i.IssueDate);
        }

        public IEnumerable<Invoice> GetAllByCompany(string company)
        {
            return invoices.Values.Where(i => i.CompanyName == company)
                .OrderByDescending(i => i.SerialNumber);
        }

        public void ExtendDeadline(DateTime dueDate, int days)
        {
            var result = invoices.Values.Where(i => i.DueDate == dueDate);

            if (result.Count() == 0)
            {
                throw new ArgumentException();
            }

            foreach (var invoice in result)
            {
                invoice.DueDate = invoice.DueDate.AddDays(days);
            }
        }
    }
}
