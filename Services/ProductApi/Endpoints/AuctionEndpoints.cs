using Microsoft.AspNetCore.Http.HttpResults;
using ProductApi.Models.DTO.Auctions;
using ProductApi.Models.Entities;
using ProductApi.Repositories.Interfaces;
using ProductApi.Utils;

namespace ProductApi.Endpoints;

public static class AuctionEndpoints
{
    public static void MapAuctionEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/auction")
            .WithTags("Auction");

        group.MapPost("", CreateAuction)
            .WithValidator<CreateAuctionDto>()
            .Produces<ResponseAuctionDto>(StatusCodes.Status201Created);

        group.MapGet("", GetAuctions)
            .Produces<List<ResponseAuctionDto>>();
        
        group.MapGet("{auctionId:guid}", GetAuctionById)
            .Produces<ResponseAuctionDto>()
            .Produces<string>(StatusCodes.Status404NotFound);

        group.MapDelete("{auctionId:guid}", DeleteAuctionById)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<string>(StatusCodes.Status404NotFound);
    }

    private static async Task<Results<Created<ResponseAuctionDto>, BadRequest<string>, NotFound<string>>> CreateAuction(
            CreateAuctionDto dto, 
            IAuctionRepository auctionRepository,
            ICategoryRepository categoryRepository)
    {
        var category = await categoryRepository.GetCategoryById(dto.CategoryId);
        if (category is null)
            return TypedResults.NotFound($"Category: {dto.CategoryId} not found");
        
        var auction = new Auction
        {
            SellerId = new Guid(), // todo (JWT access token)
            EndTime = dto.AuctionEnd,
            StartPrice = dto.StartPrice,
            Status = Status.Live,
            Category = category,
            Product = new Product
            {
                Title = dto.Title,
                Description = dto.Description,
            }
        };
        await auctionRepository.CreateAuction(auction);
        return TypedResults.Created(auction.Id.ToString(), new ResponseAuctionDto(auction)); 
    }
    
    private static async Task<Results<Ok<ResponseAuctionDto>, NotFound<string>>> GetAuctionById(
        Guid auctionId, 
        IAuctionRepository repository)
    {
        var auction = await repository.GetAuctionById(auctionId);
        return auction is not null
            ? TypedResults.Ok(new ResponseAuctionDto(auction))
            : TypedResults.NotFound($"Auction: {auctionId} not found");
    }
    
    private static async Task<IResult> GetAuctions(IAuctionRepository repository)
    {
        var auctions = await repository.GetAllActions();
        return TypedResults.Ok(auctions.Select(a => new ResponseAuctionDto(a)));
    }

    private static async Task<Results<NoContent, NotFound<string>>> DeleteAuctionById(
        Guid auctionId, 
        IAuctionRepository repository)
    {
        var auction = await repository.GetAuctionById(auctionId);
        if (auction is null) 
            return TypedResults.NotFound($"Auction: {auctionId} not found");
        await repository.DeleteAuction(auction);
        return TypedResults.NoContent();
    }
}