﻿using UdemyMicroservices.Shared;

namespace UdemyMicroservices.Order.Application.Features.Orders.GetAllOrderByUserId;

public record GetAllOrderByUserIdQuery : IRequestByServiceResult<List<GetOrdersByUserIdResponse>>;