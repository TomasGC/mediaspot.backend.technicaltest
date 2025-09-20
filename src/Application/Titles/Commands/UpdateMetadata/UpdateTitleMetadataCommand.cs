using MediatR;

namespace Mediaspot.Application.Titles.Commands.UpdateMetadata;

public sealed record UpdateTitleMetadataCommand(Guid TitleId, string Name, string OriginCountry, string OriginalLanguage, string? Description, ushort? SeasonNumber) : IRequest;
