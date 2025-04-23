using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Chat;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class ChatService : GenericService<SaveChatViewModel, ChatVm, Chat, int>, IChatService
    {

        private readonly IMapper _mapper;
        private readonly IChatRepository _chatRepository;
        public ChatService(IMapper mapper, IChatRepository chatRepository) : base(chatRepository, mapper)
        {
            _mapper = mapper;
            _chatRepository = chatRepository;
        }

        public async Task<ChatVm> AddVm(SaveChatViewModel vm)
        {
            Chat chat = await _chatRepository.AddAsync(_mapper.Map<Chat>(vm));
            return _mapper.Map<ChatVm>(chat);
        }
    }   
}

