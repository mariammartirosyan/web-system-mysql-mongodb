using System;
using System.Xml.Linq;
using Infrastructure.Repositories.MySQL;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Services.MySQL
{
	public class TourService
	{
        private readonly TourRepository _tourRepository;

        public TourService(TourRepository tourRepository)
		{
            _tourRepository = tourRepository;
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
                foreach (var includedTour in tour.IncludedTour)
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
                foreach (var transportation in tour.Transportation)
                {
                    tourViewModel.Transportation.Add(new TransportationViewModel()
                    {
                        Id = transportation.Id,
                        TourId = transportation.TourId,
                        DepartureLocation = transportation.DepartureLocation,
                        DepartureDateTime = transportation.DepartureDateTime,
                        ArrivalLocation = transportation.ArrivalLocation,
                        ArrivalDateTime = transportation.ArrivalDateTime,
                        Type = transportation.Type,
                        Price = transportation.Price
                    });

                }
                foreach (var accommodation in tour.Accommodation)
                {
                    tourViewModel.Accommodation.Add(new AccommodationViewModel()
                    {
                        TourId = accommodation.TourId,
                        Location = accommodation.Location,
                        CheckInDateTime = accommodation.CheckInDateTime,
                        CheckOutDateTime = accommodation.CheckOutDateTime,
                        Type = accommodation.Type,
                        Price = accommodation.Price
                    });

                }
                foreach (var attraction in tour.TouristAttraction)
                {
                    var touristAttraction = new TouristAttractionViewModel()
                    {
                        Name = attraction.Name,
                        Description = attraction.Description,
                        Location = attraction.Location
                    };


                    foreach(var attractionType in attraction.AttractionType)
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

