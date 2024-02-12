using DiscountGrpc.Entities;
using DiscountGrpc.Repositories;
using Grpc.Core;

namespace DiscountGrpc.Services;

public class DiscountService(IDiscountRepository repository) : DiscountProto.DiscountProtoBase
{
    private DiscountResponse ToDiscountResponse(Discount discount)
    {
        return new DiscountResponse
        {
            DiscountId = discount.Id.ToString(),
            ProductId = discount.ProductId.ToString(),
            Code = discount.Code,
            Amount = discount.Amount
        };
    }

    private async Task<Discount> FindDiscountByProductId(Guid productId)
    {
        return await repository.GetDiscountByProductId(productId)
               ?? throw new RpcException(new Status(
                   StatusCode.NotFound,
                   $"Discount: {productId} not found"));
    }

    private async Task<Discount> FindDiscountByCode(string code)
    {
        return await repository.GetDiscountByCode(code)
               ?? throw new RpcException(new Status(
                   StatusCode.NotFound,
                   $"Discount: {code} not found"));
    }
    
    public override async Task<DiscountResponse> CreateDiscount(
        CreateDiscountRequest request,  
        ServerCallContext context)
    {
        if (await repository.GetDiscountByCode(request.Code) is not null)
            throw new RpcException(new Status(
                StatusCode.AlreadyExists, 
                $"Discount: {request.Code} already exists")
            );
        
        var discount = new Discount
        {
            ProductId =  Guid.Parse(request.ProductId),
            Code = request.Code,
            Amount = request.Amount
        };
        await repository.CreateDiscount(discount);
        return await Task.FromResult(ToDiscountResponse(discount));
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(
        DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var discount = await FindDiscountByProductId(Guid.Parse(request.ProductId));
        await repository.DeleteDiscount(discount);
        return await Task.FromResult(new DeleteDiscountResponse { DiscountId = discount.Id.ToString() });
    }

    public override async Task<DiscountResponse> GetDiscountByProductId(
        GetDiscountByProductIdRequest request,
        ServerCallContext context)
    {
        var discount = await FindDiscountByProductId(Guid.Parse(request.ProductId));
        return await Task.FromResult(ToDiscountResponse(discount));
    }

    public override async Task<DiscountResponse> GetDiscountByCode(
        GetDiscountByCodeRequest request,
        ServerCallContext context)
    {
        var discount = await FindDiscountByCode(request.Code);
        return await Task.FromResult(ToDiscountResponse(discount));
    }
}
