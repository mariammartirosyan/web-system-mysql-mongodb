using System;
using Infrastructure.Repositories.NoSQL;
using WebApp.Models;

namespace WebApp.Services.NoSQL
{
	public class TourService
	{
        private readonly TourRepository _tourRepository;
        private readonly TouristAttractionRepository _attractionRepository;

        public TourService(TourRepository tourRepository, TouristAttractionRepository attractionRepository)
        {
            _tourRepository = tourRepository;
            _attractionRepository = attractionRepository;
        }
        public TourViewModel GetById(int id)
        {
            TourViewModel tourViewModel = null;
            var tour = _tourRepository.GetById(id);

            if (tour != null)
            {
                tourViewModel = new TourViewModel()
                {
                    Id = tour.Id,
                    Description = tour.Description,
                    StartDate = tour.StartDate,
                    EndDate = tour.EndDate,
                    Price = tour.Price
                };
                foreach (var includedTourId in tour.IncludedTourIds)
                {
                    var includedTour = _tourRepository.GetById(includedTourId);
                    if (includedTour != null)
                    {
                        tourViewModel.IncludedTour.Add(new TourViewModel()
                        {
                            Id = includedTour.Id,
                            Description = includedTour.Description,
                            StartDate = includedTour.StartDate,
                            EndDate = includedTour.EndDate,
                            Price = includedTour.Price
                        });
                    }
                    
                }
                foreach (var transportation in tour.Transportations)
                {
                    tourViewModel.Transportation.Add(new TransportationViewModel()
                    {
                        DepartureLocation = transportation.DepartureLocation,
                        DepartureDateTime = transportation.DepartureDateTime,
                        ArrivalLocation = transportation.ArrivalLocation,
                        ArrivalDateTime = transportation.ArrivalDateTime,
                        Type = transportation.Type,
                        Price = transportation.Price
                    });

                }
                foreach (var accommodation in tour.Accommodations)
                {
                    tourViewModel.Accommodation.Add(new AccommodationViewModel()
                    {
                        Location = accommodation.Location,
                        CheckInDateTime = accommodation.CheckInDateTime,
                        CheckOutDateTime = accommodation.CheckOutDateTime,
                        Type = accommodation.Type,
                        Price = accommodation.Price
                    });

                }
                foreach (var attractionId in tour.TouristAttractionIds)
                {

                    var attraction = _attractionRepository.GetById(id);
                    if (attraction != null)
                    {
                        var touristAttraction = new TouristAttractionViewModel()
                        {
                            Name = attraction.Name,
                            Description = attraction.Description,
                            Location = attraction.Location
                        };


                        foreach (var attractionType in attraction.AttractionTypes)
                        {
                            touristAttraction.AttractionType.Add(new AttractionTypeViewModel()
                            {
                                Name = attractionType.Name,
                                Description = attractionType.Description
                            });
                        }

                        tourViewModel.TouristAttraction.Add(touristAttraction);
                    }

                }
            }

            return tourViewModel;
        }
        public List<TourViewModel> GetAll()
        {
            var tours = _tourRepository.GetAll();
            var tourViewModelList = new List<TourViewModel>();
            foreach (var tour in tours)
            {
                tourViewModelList.Add(new TourViewModel()
                {
                    Id = tour.Id,
                    Description = tour.Description,
                    Price = tour.Price,
                    StartDate = tour.StartDate,
                    EndDate = tour.EndDate
                });
            }
            return tourViewModelList;
        }
    }
}

