using Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User
{
    public class ResetPassword : BaseEntity
    {
        /// <summary>
        /// Логин пользователя, кототрый хочет сбросить пароль
        /// </summary>
        [MaxLength(150)]
        public string Login { get; set; }

        /// <summary>
        /// Код подтверждения. Думаю, пока будет 6 символов. 10 оставил, чтобы потом не расширять
        /// </summary>
        [MaxLength(10)]
        public string ConfirmationCode { get; set; }

        /// <summary>
        /// Время действия. На данный момент будет рассчитваться по формуле: DateTime.Now + 30 минут
        /// </summary>
        public DateTime TimeLimit { get; set; }

        /// <summary>
        /// Время создания запроса на восстановление пароля
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Новый хэш пароля
        /// </summary>
        public string NewPasswordHash { get; set; }
    }
}
