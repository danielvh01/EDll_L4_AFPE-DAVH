﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDDll_L4_AFPE_DAVH.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class START : ControllerBase
    {
        [HttpGet]

        public string Get()
        {
            #region drawings
            string start = @" 
                                                                                                                              .
                                                                                          `.

                                                                                     ...
                                                                                        `.
                                                                                  ..
                                                                                    `.
                                                                            `.        `.
                                                                         ___`.\.//
                                                                            `---.---
                                                                           /     \.--
                                                                          /       \-
                                                                         |   /\    \
                                                                         |\==/\==/  |
                                                                         | `@'`@'  .--.
                                                                  .--------.           )
                                                                .'             .   `._/
                                                               /               |     \
                                                              .               /       |
                                                              |              /        |
                                                              |            .'         |   .--.
                                                             .'`.        .'_          |  /    \
                                                           .'    `.__.--'.--`.       / .'      |
                                                         .'            .|    \\     |_/        |
                                                       .'            .' |     \\               |
                                                     .-`.           /   |      .      __       |
                                                   .'    `.     \   |   `           .'  )      \
                                                  /        \   / \  |            .-'   /       |
                                                 (  /       \ /   \ |                 |        |
                                                  \/         (     \/                 |        |
                                                  (  /        )    /                 /   _.----|
                                                   \/   //   /   .'                  |.-'       `
                                                   (   /(   /   /                    /      `.   |
                                                    `.(  `-')  .---.                |    `.   `._/
                                                       `._.'  /     `.   .---.      |  .   `._.'
                                                              |       \ /     `.     \  `.___.'
                                                              |        Y        `.    `.___.'
                                                              |      . |          \         \
                                                              |       `|           \         |
                                                              |        |       .    \        |
                                                              |        |        \    \       |
                                                            .--.       |         \           |
                                                           /    `.  .----.        \          /
                                                          /       \/      \        \        /
                                                          |       |        \       |       /
                                                           \      |    @    \   `-. \     /
                                                            \      \         \     \|.__.'
                                                             \      \         \     |
                                                              \      \         \    |
                                                               \      \         \   |
                                                                \    .'`.        \  |
                                                                 `.-'    `.    _.'\ |
                                                                   |       `.-'    ||
                                                              .     \     . `.     ||      .'
                                                               `.    `-.-'    `.__.'     .'
                                                                 `.                    .'
                                                             .                       .'
                                                              `.
                                                                                           .-'
                                                                                        .-'

                                                      \                 \
                                                       \         ..      \
                                                        \       /  `-.--.___ __.-.___
                                                `-.      \     /  #   `-._.-'    \   `--.__
                                                   `-.        /  ####    /   ###  \        `.
                                                ________     /  #### ############  |       _|           .'
                                                            |\ #### ##############  \__.--' |    /    .'
                                                            | ####################  |       |   /   .'
                                                            | #### ###############  |       |  /
                                                            | #### ###############  |      /|      ----
                                                          . | #### ###############  |    .'<    ____
                                                        .'  | ####################  | _.'-'\|
                                                      .'    |   ##################  |       |
                                                             `.   ################  |       |
                                                               `.    ############   |       | ----
                                                              ___`.     #####     _..____.-'     .
                                                             |`-._ `-._       _.-'    \\\         `.
                                                          .'`-._  `-._ `-._.-'`--.___.-' \          `.
                                                        .' .. . `-._  `-._        ___.---'|   \   \
                                                      .' .. . .. .  `-._  `-.__.-'        |    \   \
                                                     |`-. . ..  . .. .  `-._|             |     \   \
                                                     |   `-._ . ..  . ..   .'            _|
                                                      `-._   `-._ . ..   .' |      __.--'
                                                          `-._   `-._  .' .'|__.--'
                                                              `-._   `' .'
                                                                  `-._.'

         ";

            string info = @"
                    
                        █ █▄░█   █▀█ █▀█ █▀▄ █▀▀ █▀█   ▀█▀ █▀█   █░█ █▀ █▀▀   ▀█▀ █░█ █▀▀   █░█ █░█ █▀▀ █▀▀ █▀▄▀█ ▄▀█ █▄░█ █▄░█
                        █ █░▀█   █▄█ █▀▄ █▄▀ ██▄ █▀▄   ░█░ █▄█   █▄█ ▄█ ██▄   ░█░ █▀█ ██▄   █▀█ █▄█ █▀░ █▀░ █░▀░█ █▀█ █░▀█ █░▀█

                        █▀▀ █▀█ █▀▄▀█ █▀█ █▀█ █▀▀ █▀ █▀ █▀█ █▀█ ░   █▄█ █▀█ █░█   █▀▄▀█ █░█ █▀ ▀█▀   █▄░█ █▀▀ █▀▀ █▀▄   ▄▀█ █▄░█   ▄▀█ █▀█ █
                        █▄▄ █▄█ █░▀░█ █▀▀ █▀▄ ██▄ ▄█ ▄█ █▄█ █▀▄ █   ░█░ █▄█ █▄█   █░▀░█ █▄█ ▄█ ░█░   █░▀█ ██▄ ██▄ █▄▀   █▀█ █░▀█   █▀█ █▀▀ █

                        █▀█ █░░ ▄▀█ ▀█▀ █▀▀ █▀█ █▀█ █▀▄▀█   ▄▀ █░░ █ █▄▀ █▀▀   █▀█ █▀█ █▀ ▀█▀ █▀▄▀█ ▄▀█ █▄░█ ▀▄   █▀▀ █▀█ █▀█   █░█ █▀ █ █▄░█ █▀▀
                        █▀▀ █▄▄ █▀█ ░█░ █▀░ █▄█ █▀▄ █░▀░█   ▀▄ █▄▄ █ █░█ ██▄   █▀▀ █▄█ ▄█ ░█░ █░▀░█ █▀█ █░▀█ ▄▀   █▀░ █▄█ █▀▄   █▄█ ▄█ █ █░▀█ █▄█

                        ▄▀█ █▀█ █ █▀ ░
                        █▀█ █▀▀ █ ▄█ ▄                                                                        
            ";
            #endregion
            return info + start;
        }
    }
}