namespace OpenHack
{
    public static class ConversionExtensions
    {
        public static FeedbackViewModel ToViewModel(this RatingModel model) => new FeedbackViewModel
        {
            Id = model.Id,
            UserId = model.UserId,
            ProductId = model.ProductId,
            TimeStamp = model.TimeStamp,
            LocationName = model.LocationName,
            Rating = model.Rating,
            UserNotes = model.UserNotes,
        };

        public static RatingModel ToModel(this FeedbackViewModel viewModel) => new RatingModel
        {
            Id = viewModel.Id,
            UserId = viewModel.UserId,
            ProductId = viewModel.ProductId,
            TimeStamp = viewModel.TimeStamp,
            LocationName = viewModel.LocationName,
            Rating = viewModel.Rating,
            UserNotes = viewModel.UserNotes,
        };
    }
}
