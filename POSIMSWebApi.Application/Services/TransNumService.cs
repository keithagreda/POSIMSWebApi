using Domain.Enums;
using Domain.Interfaces;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSIMSWebApi.Application.Services
{
    public class TransNumService
    {
        private readonly Dictionary<TransactionEnum, Func<int, int?, string>> _generators;

        public TransNumService()
        {
            _generators = new Dictionary<TransactionEnum, Func<int, int?, string>>
            {
                { TransactionEnum.Sales, (productId, _) => GenerateSalesNumber() },
                { TransactionEnum.Receiving, (productId, count) => GenerateProductBasedNumber("REC", productId, count) },
                { TransactionEnum.Damage, (productId, count) => GenerateProductBasedNumber("DAM", productId, count) },
                { TransactionEnum.Return, (productId, count) => GenerateProductBasedNumber("RET", productId, count) }
            };
        }

        public string GenerateTransactionNumber(TransactionEnum transactionType, int productId = 0, int? productCount = null)
        {
            if (!_generators.ContainsKey(transactionType))
                throw new InvalidOperationException("Unsupported transaction type");

            return _generators[transactionType](productId, productCount);
        }

        private string GenerateSalesNumber()
        {
            return $"SAL-{DateTime.Now:yyyyMMddHHmmss}";
        }

        private string GenerateProductBasedNumber(string prefix, int productId, int? productCount)
        {
            if (productId <= 0)
                throw new ArgumentException("Product ID is required for this transaction type.");

            var productCode = GetProductCode(productId);
            var countPart = productCount.HasValue ? $"-{productCount.Value}" : string.Empty;

            return $"{prefix}-{productCode}-{DateTime.Now:yyyyMMddHHmmss}{countPart}";
        }

        private string GetProductCode(int productId)
        {
            // Simulated fetch from repository or database
            return $"PROD{productId:D4}";
        }
    }

}
