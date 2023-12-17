﻿using Entities.Abstractions;
using System;
using System.ComponentModel.DataAnnotations;

namespace User;

public class User : BaseEntity
{
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
    /// Исключение - UserDto_WithLoginPassword. Там эти данные нужны для работы.
    /// </summary>
    [MaxLength(150)]
    public string Login { get; set; }

    /// <summary>
    /// Хеш пароля пользователя (будем хранить пароль в БД напрямую - нам менторы вставят Ай-Яй-Яй!)
    /// Исключение - UserDto_WithLoginPassword. Там эти данные нужны для работы.
    /// </summary>
    [MaxLength(150)]
    public string Password { get; set; }
}