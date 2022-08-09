using ACYZenWebApp1.Models;

namespace ACYZenWebApp1;

public class SampleData
{
    public static void Initialize(DZenActionContext context)
        {
            if (!context.ZenActions.Any())
            {
                context.ZenActions.AddRange(
                    new ZenAction
                    {
                        Name = "Авторегистрация новых Дзен аккаунтов",
                        Description = "Автоматическая регистрация новых Яндекс аккаунтов (с удалением номера, созданием канала на Дзене)",
                        Price = 600
                    },
                    new ZenAction
                    {
                        Name = "Автопостинг статей",
                        Description = "Автоматический постинг статей на Дзен каналах",
                        Price = 350
                    },
                    new ZenAction
                    {
                        Name = "Рефреш аккаунтов",
                        Description = "Автоматическое изменение пароля (привязка и удаление нового номера телефона)",
                        Price = 500
                    }
                );
                context.SaveChanges();
            }
        }
    }