﻿using Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace User.Dto;

/// <summary>
/// User Data Transfer Object
/// !!! Отличается тем, что при (создании)/(обновлении логина пароля пользователя) нам НУЖНЫ логин и пароль. Так что при данном событии мы их передаём
/// </summary>
public class UserDto_WithLoginPassword
{
    /// <summary>
    /// Id будем использовать для того, чтобы обращаться к пользователю напрямую, минуя поиск по остальным параметрам
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Блокирована ли учётная запись;
    /// false - всё Ок;
    /// true - пользователь накосячил и наказан;
    /// </summary>
    public bool Blocked { get; set; }

    /// <summary>
    /// Половая принадлежность;
    /// 0 (false) - девочка;
    /// 1 (true)  - мальчик;
    /// Думаю комментарии излишни =)
    /// </summary>
    public bool Gender { get; set; }

    /// <summary>
    /// Дата рождения пользователя
    /// </summary>
    public DateTime DateOfBirth { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество пользователя
    /// </summary>
    public string Surname { get; set; }

    /// <summary>
    /// Пользователь о себе
    /// </summary>
    [MaxLength(250)]
    public string About { get; set; }

    /// <summary>
    /// Кого ищет пользователь
    /// </summary>
    [MaxLength(250)]
    public string LookingFor { get; set; }

    /// <summary>
    /// Семейное положение;
    /// Свободен = 0;
    /// ВБраке = 1;
    /// Разведён = 3;
    /// </summary>
    public FamilyStatus FamilyStatus { get; set; }

    /// <summary>
    /// Наличие детей;
    /// false - отсутствуют;
    /// true  - присутствуют;
    /// </summary>
    public bool HaveChildren { get; set; }

    /// <summary>
    /// Логин пользователя. Не передавать в DTO!
    /// </summary>
    [MaxLength(150)]
    public string Login { get; set; }

    /// <summary>
    /// Хеш пароля пользователя (будем хранить пароль в БД напрямую - нам менторы вставят Ай-Яй-Яй!)
    /// </summary>
    [MaxLength(150)]
    public string Password { get; set; }



    /// <summary>
    /// Имя пользователя на сайте знакомств (которое он хочет высавить "на обозрение" для других пользователей
    /// </summary>
    [MaxLength(150)]
    public string UserName { get; set; }

    /// <summary>
    /// Возраст пользователя
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Подтверждён ли аккаунт пользователя? Нет - не лезь сюда за бабами! Совсем усилий не приложил!..
    /// </summary>
    public bool Confirmed { get; set; }

    /// <summary>
    /// Дата подтверждения пользователем своего аккаунта
    /// Может быть NULL
    /// </summary>
    public DateTime? Confirmed_At { get; set; }

    /// <summary>
    /// Email пользователя. Может быть использован для восстановления пароля, подтверждения регистрации и т.д.
    /// </summary>
    public string Email { get; set; }
}