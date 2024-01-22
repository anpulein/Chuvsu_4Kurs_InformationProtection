// $(document).ready(function () {
//     // Находим выпадающий список по его id
//     var dropdown = $('#listbox-label');
//
//     // Находим все элементы списка
//     var options = dropdown.find('li');
//
//     // Добавляем обработчик события клика на элемент списка
//     options.click(function () {
//         // Получаем значение выбранного элемента (здесь это будет значение Labs enum)
//         var selectedLab = $(this).attr('data-selected');
//
//         // Проверяем, был ли выбран элемент
//         if (selectedLab === 'true') {
//             // Отправляем AJAX-запрос на сервер с выбранным значением
//             $.ajax({
//                 url: '/Home/Index', // Замените на свой URL
//                 type: 'GET',
//                 data: { lab: $(this).attr('asp-route-lab') }, // Замените на свои параметры
//                 success: function (result) {
//                     // Здесь вы можете обработать результат, который вернул сервер
//                     // Например, обновить содержимое страницы
//                     console.log(result);
//                 },
//                 error: function (error) {
//                     // Обработка ошибок
//                     console.error(error);
//                 }
//             });
//         }
//     });
// });

$(document).ready(function () {
    // Добавить обработчик клика к элементам списка
    $("li").click(function (event) {
        // Отменить стандартное действие (навигацию по ссылке)
        event.preventDefault();

        // Получить URL из ссылки внутри элемента списка
        // Перейти по URL
        var url = $(this).find("a").attr("href");
        

        // Выполнить AJAX-запрос для получения обновленного частичного представления
        $.ajax({
            url: url,
            type: "GET",
            success: function (data) {
                // Обновить компонент на странице с полученными данными
                $("#labData").html(data);
            },
            error: function () {
                console.error("Произошла ошибка при выполнении AJAX-запроса.");
            }
        });
    });
});