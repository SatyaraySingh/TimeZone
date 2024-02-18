using Microsoft.AspNetCore.Server.IIS;
using System.Net;
using System.Text.Json;
using System.Web.Http;
using System.Web.Mvc;
using TimeZone.Entities;
using TimeZone.Repositories;
using TimeZone.Requests;
using TimeZone.Responses;

namespace TimeZone.Services
{
    public class GuestService
    {
        private readonly GuestRepository _guestRepository;

        public GuestService(GuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestResponse> Get(Guid id)
        {
            var guest = await _guestRepository.Get(id);

            var response = new GuestResponse
            {
                Id = guest.Id,
                Title = guest.Title,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumbers
                //PhoneNumbers = (List<PhoneResponse>)guest.PhoneNumbers.Select(phone => new PhoneResponse
                //{
                //    Id = phone.Id,
                //    Number = phone.Number,
                //}),
            };

            return response;
        }

        public async Task<List<GuestResponse>> GetAll()
        {
            var guests = await _guestRepository.GetAll();

            var response = guests.Select(guest => new GuestResponse
            {
                Id = guest.Id,
                Title = guest.Title,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumbers

                //PhoneNumbers = (List<PhoneResponse>)guest.PhoneNumbers.Select(phone => new PhoneResponse
                //{
                //    Id = phone.Id,
                //    Number = phone.Number,
                //}),
            }).ToList();

            return response;
        }

        public async Task<GuestResponse> Create(GuestRequest request)
        {
            var guest = new Guest
            {
                Title = request.Title,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PhoneNumbers = SerializeMethod(request.PhoneNumber),
                //PhoneNumbers = request.PhoneNumbers,
            };

            await _guestRepository.Create(guest);

            return new GuestResponse
            {
                Id = guest.Id,
                Title = guest.Title,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumbers,
                //PhoneNumbers = (List<PhoneResponse>)guest.PhoneNumbers.Select(phone => new PhoneResponse { Id = phone.Id, Number = phone.Number }),
            };
        }

        public async Task<GuestResponse> Update(Guid id, GuestRequest request)
        {
            var guest = await _guestRepository.Get(id);

            guest.Title = request.Title;
            guest.FirstName = request.FirstName;
            guest.LastName = request.LastName;
            guest.BirthDate = request.BirthDate;
            guest.Email = request.Email;
            guest.PhoneNumbers = SerializeMethod(request.PhoneNumber);
            //guest.PhoneNumbers = (List<Phone>)request.PhoneNumbers.Select(phone => new PhoneRequest {  Number = phone.Number });

            await _guestRepository.Update(guest);

            return new GuestResponse
            {
                Id = guest.Id,
                Title = guest.Title,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumbers,
                //PhoneNumbers = (List<PhoneResponse>)guest.PhoneNumbers.Select(phone => new PhoneResponse { Id = phone.Id, Number = phone.Number }),
            };
        }

        public async Task Delete(Guid id)
        {
            await _guestRepository.Delete(id);
        }

        public async Task<GuestResponse> AddPhone(Guid id, string request)
        {
            var guest = await _guestRepository.Get(id);


            var numbers = DeserializeMethod(guest.PhoneNumbers);
            if (numbers.Contains(request))
            {
                return null;
            }
            numbers.Add(request);
            guest.PhoneNumbers = SerializeMethod(numbers);
            //guest.PhoneNumbers = (List<Phone>)request.PhoneNumbers.Select(phone => new PhoneRequest {  Number = phone.Number });

            await _guestRepository.Update(guest);

            return new GuestResponse
            {
                Id = guest.Id,
                Title = guest.Title,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                BirthDate = guest.BirthDate,
                Email = guest.Email,
                PhoneNumber = guest.PhoneNumbers,
                //PhoneNumbers = (List<PhoneResponse>)guest.PhoneNumbers.Select(phone => new PhoneResponse { Id = phone.Id, Number = phone.Number }),
            };
        }

        public string SerializeMethod(List<string> Numbers)
        {
            return JsonSerializer.Serialize(Numbers);
        }

        public string SerializeMethod(List<GuestResponse> guestResponses)
        {
            return JsonSerializer.Serialize(guestResponses);
        }

        public string SerializeMethod(GuestResponse guestResponses)
        {
            return JsonSerializer.Serialize(guestResponses);
        }

        public List<string> DeserializeMethod(string Numbers)
        {
            return JsonSerializer.Deserialize<List<string>>(Numbers);
        }

        public bool Isduplicate(List<string> Numbers)
        {
            return Numbers.GroupBy(n => n).Any(c => c.Count() > 1);
        }

    }
}
