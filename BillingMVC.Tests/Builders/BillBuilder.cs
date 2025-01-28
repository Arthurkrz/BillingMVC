using BillingMVC.Core.Entities;
using BillingMVC.Core.Enum;
using Bogus;
using System;

namespace BillingMVC.Tests.Builders
{
    internal class BillBuilder
    {
        private readonly Bill _bill;
        private readonly Faker _faker;

        public BillBuilder()
        {
            _bill = new Bill();
            _faker = new Faker();
        }

        public Bill Build() => _bill;

        public BillBuilder ValidName
                           (string name = null)
        {
            if (name == null)
                name = _faker.Name.FullName();

            _bill.Name = name;
            return this;
        }
        
        public BillBuilder ValidCurrency
                           (Currency currency = 
                            Currency.NA)
        {
            if (currency == Currency.NA)
                currency = Currency.Euro;

            _bill.Currency = currency;
            return this;
        }

        public BillBuilder ValidValue
                           (double value = 0)
        {
            if (value == 0)
                value = _faker.Random.Double(1.0, 999999.0);

            _bill.Value = value;
            return this;
        }

        public BillBuilder ValidType
                           (BillType type = 
                            BillType.Select)
        {
            if (type == BillType.Select)
                type = BillType.Food;

            _bill.Type = type;
            return this;
        }

        public BillBuilder ValidExpirationDate
                           (DateTime exp = default)
        {
            if (exp == default)
                exp = _faker.Date.Future(1, 
                      DateTime.Now.AddMonths(-6));

            _bill.ExpirationDate = exp;
            return this;
        }

        public BillBuilder ValidSource
                           (string source = null)
        {
            if (source == null)
                source = _faker.Name.FullName();

            _bill.Source = source;
            return this;
        }

        public BillBuilder ValidIsPaid
                           (CustomBoolean isPaid = 
                            CustomBoolean.NA)
        {
            if (isPaid == CustomBoolean.NA)
                isPaid = CustomBoolean.Yes;

            _bill.IsPaid = isPaid;
            return this;
        }

        public BillBuilder ValidIsRecurring
                           (CustomBoolean isRecurring = 
                            CustomBoolean.NA)
        {
            if (isRecurring == CustomBoolean.NA)
                isRecurring = CustomBoolean.No;

            _bill.IsRecurring = isRecurring;
            return this;
        }

        public BillBuilder InvalidName
                           (string invalidName = null)
        {
            if (invalidName == null)
                invalidName = "a";

            _bill.Name = invalidName;
            return this;
        }

        public BillBuilder InvalidCurrency
                           (Currency invalidCurrency = 
                            Currency.NA)
        {
            if (invalidCurrency == Currency.NA)
                _bill.Currency = invalidCurrency;

            return this;
        }

        public BillBuilder InvalidValue
                           (double invalidValue = 0)
        {
            if (invalidValue == 0)
                _bill.Value = invalidValue;

            return this;
        }

        public BillBuilder InvalidType
                           (BillType invalidType = 
                            BillType.Select)
        {
            if (invalidType == BillType.Select)
                _bill.Type = invalidType;

            return this;
        }

        public BillBuilder InvalidExpirationDate
                           (DateTime invalidExp = default)
        {
            if (invalidExp == default)
                _bill.ExpirationDate = invalidExp;

            return this;
        }

        public BillBuilder InvalidSource
                           (string invalidSource = null)
        {
            if (invalidSource == null)
                _bill.Source = invalidSource;

            return this;
        }

        public BillBuilder InvalidIsPaid
                           (CustomBoolean invalidIsPaid = 
                            CustomBoolean.NA)
        {
            if (invalidIsPaid == CustomBoolean.NA)
                _bill.IsPaid = invalidIsPaid;

            return this;
        }

        public BillBuilder InvalidIsRecurring
                           (CustomBoolean invalidIsRecurring = 
                            CustomBoolean.NA)
        {
            if (invalidIsRecurring == CustomBoolean.NA)
                _bill.IsRecurring = invalidIsRecurring;

            return this;
        }

        public BillBuilder OldBillSixMonths
                           (DateTime exp6m = default)
        {
            if (exp6m == default)
                exp6m = _faker.Date.Past(1, DateTime.Now.AddMonths(6));

            _bill.ExpirationDate = exp6m;
            return this;
        }

        public BillBuilder OldBillOneYear
                           (DateTime exp1y = default)
        {
            if (exp1y == default)
                exp1y = _faker.Date.Past(1, DateTime.Now);

            _bill.ExpirationDate = exp1y;
            return this;
        }
    }
}
