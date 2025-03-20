﻿global using Carter;
global using MediatR;
global using Mapster;
global using FluentValidation;
global using MassTransit;
global using Humanizer;
global using HealthChecks.UI.Client;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.FeatureManagement;
global using BuildingBlocks.CQRS;
global using BuildingBlocks.Behaviors;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Web;
global using BuildingBlocks.Core.Event;
global using BuildingBlocks.Core.Model;
global using BuildingBlocks.Events.ProfileManagement;
global using BuildingBlocks.EFCore.Interceptors;
global using BuildingBlocks.Exceptions.Handler;
global using BuildingBlocks.Jwt;
global using BuildingBlocks.MassTransit;
global using ProfileManagement.API.Profiles.Enums;
global using ProfileManagement.API.Profiles.Models;
global using ProfileManagement.API.Profiles.ValueObjects;
global using ProfileManagement.API.Sports.Models;
global using ProfileManagement.API.Sports.ValueObjects;
global using ProfileManagement.API.Data;
global using ProfileManagement.API.Data.Repositories;
global using ProfileManagement.API.GrpcServer.Services;
global using ProfileManagement.API.Profiles.Mapster;
global using BuildingBlocks.Events.Identity;
global using BuildingBlocks.Logging;
global using BuildingBlocks.Mongo;
global using Microsoft.Extensions.Options;
global using MongoDB.Bson;
global using MongoDB.Bson.Serialization.Attributes;
global using MongoDB.Driver;
global using ProfileManagement.API.Profiles.Exceptions;
global using ProfileManagement.API.Profiles.Dtos;
global using ProfileManagement.API.Profiles.Features.Commands.AddProfileSport;
global using ProfileManagement.API.Profiles.Features.Commands.CreateProfile;
global using ProfileManagement.API.Profiles.Features.Commands.RemoveProfileSport;
global using ProfileManagement.API.Profiles.Features.Commands.UpdateProfile;
global using ProfileManagement.API.Profiles.Models.ReadModels;
global using ProfileManagement.API.Sports.Exceptions;
global using ProfileManagement.API.Sports.Dtos;