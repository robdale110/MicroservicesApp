﻿using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Basket.Api.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService) =>
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));

        public async Task<CouponModel> GetDiscount(string productName) =>
            await _discountProtoService.GetDiscountAsync(new GetDiscountRequest { ProductName = productName });
    }
}
