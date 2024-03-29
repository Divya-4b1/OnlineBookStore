﻿using OnlineBookStore.Interfaces;
using OnlineBookStore.Models;
using OnlineBookStore.Models.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace OnlineBookStore.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<string, User> _repository;
        private readonly ITokenService _tokenService;
        public UserService(IRepository<string, User> repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public UserDTO Login(UserDTO userDTO)
        {
            var user = _repository.GetById(userDTO.Username);
            if (user != null)
            {
                HMACSHA512 hmac = new HMACSHA512(user.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (user.Password[i] != userpass[i])
                        return null;
                }
                userDTO.Email = user.Email;
                userDTO.Role = user.Role;
                userDTO.Token = _tokenService.GetToken(userDTO);
                userDTO.Password = "";



                return userDTO;
            }
            return null;
        }

        public UserDTO Register(UserDTO userDTO)
        {
            HMACSHA512 hmac = new HMACSHA512();
            User user = new User()
            {
                Username = userDTO.Username,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                Email = userDTO.Email,
                Key = hmac.Key,
                Role = userDTO.Role
            };

            var result = _repository.Add(user);
            if (result != null)
            {
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }
    }
    }

