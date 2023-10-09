using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="email">Email model</param>
        /// <returns>The <see cref="bool"/></returns>
        Task<bool> SendEmailAsync(Email email);
    }
}
