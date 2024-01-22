// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
function clearParagraph(dynamicOutput) {
    if (dynamicOutput.childElementCount !== 0) {
        // Удаляем все дочерние элементы из <div>
        while (dynamicOutput.firstChild) {
            dynamicOutput.removeChild(dynamicOutput.firstChild);
        }
    }
}


async function showTerminalText(result, dynamicOutput) {
    const arrayOfStrings = result.split('\n');

    for (let i = 0; i < arrayOfStrings.length; i++) {
        // Создаем новый элемент <p>
        const newParagraph = document.createElement("p");

        // Устанавливаем атрибуты для нового элемента
        newParagraph.id = "dynamic-output-" + i;
        newParagraph.className = "pb-1";

        // Добавляем CSS свойство для переноса текста
        newParagraph.style.whiteSpace = "pre-wrap";

        // Добавляем новый элемент в документ
        dynamicOutput.appendChild(newParagraph);

        newParagraph.textContent = "";
        let text = arrayOfStrings[i];
        let charIndex = 0;

        await new Promise((resolve) => {
            function addChar() {
                newParagraph.textContent += text[charIndex];
                charIndex++;

                if (charIndex < text.length) {
                    setTimeout(addChar, 0.1); // Задержка в миллисекундах
                } else {
                    resolve(); // Завершаем Promise после вывода всего текста
                }
            }

            addChar();
        });
    }
}




$(document).ready(function () {
    $('body').on("click", ".btn-encoder", function () {
        const dynamicOutput = document.getElementById("console");
        const inputEl = $('.input-text > .relative > input');
        const url = inputEl.data("url");
        const inputData = {};
        const typeCoder = "encoder";

        // Получил значения из всех ваших input и добавьте их в объект inputData
        inputEl.each(function () {
            const propertyName = $(this).data("property-name");
            inputData[propertyName] = $(this).val();
        });

        // Добавил также значение поля Type из вашей модели
        inputData["Type"] = inputEl.data("type"); // Подставил значение Type
        inputData["TypeCoder"] = typeCoder;

        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(inputData),
            success: function (result) {
                // Удаляем все дочерние элементы из <div>
                clearParagraph(dynamicOutput);
                
                showTerminalText(result, dynamicOutput).then(r => r);
            },
            error: function (error) {
                // Обработка ошибки
                console.error(error);
            }
        });
    });
});

$(document).ready(function () {
    $('body').on("click", ".btn-decoder", function () {
        const dynamicOutput = document.getElementById("console");
        const inputEl = $('.input-text > .relative > input');
        const url = inputEl.data("url");
        const inputData = {};
        const typeCoder = "decoder";

        // Получил значения из всех ваших input и добавил их в объект inputData
        inputEl.each(function () {
            const propertyName = $(this).data("property-name");
            inputData[propertyName] = $(this).val();
        });

        // Добавил также значение поля Type из вашей модели
        inputData["Type"] = inputEl.data("type"); // Подставил значение Type
        inputData["TypeCoder"] = typeCoder;

        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(inputData),
            success: function (result) {
                // Удаляем все дочерние элементы из <div>
                clearParagraph(dynamicOutput);
                
                showTerminalText(result, dynamicOutput).then(r => r);
            },
            error: function (error) {
                // Обработка ошибки
                console.error(error);
            }
        });
    });
});



// list
const spanClass =
    "text-indigo-600 group-focus:text-white group-hover:text-white absolute inset-y-0 right-0 flex items-center pr-4";

const svgAttr = {
    class: "h-5 w-5",
    viewBox: "0 0 20 20",
    fill: "currentColor",
    aria_hidden: "true",
};

const pathAttr = {
    fill_rule: "evenodd",
    d: "M16.704 4.153a.75.75 0 01.143 1.052l-8 10.5a.75.75 0 01-1.127.075l-4.5-4.5a.75.75 0 011.06-1.06l3.894 3.893 7.48-9.817a.75.75 0 011.05-.143z",
    clip_rule: "evenodd",
};


function createCheckMark() {
    let span = createElement("span", {
        class: spanClass,
    });
    let svg = createSvgElement("svg", svgAttr);
    let path = createSvgElement("path", pathAttr);

    svg.appendChild(path);
    span.appendChild(svg);
    return span;
}

function createSvgElement(name, attr) {
    let element = document.createElementNS("http://www.w3.org/2000/svg", name);

    for (let key in attr) {
        element.setAttribute(key, attr[key]);
    }

    return element;
}

function createElement(name, attr) {
    let element = document.createElement(name);

    for (let key in attr) {
        element.setAttribute(key, attr[key]);
    }

    return element;
}

function closeMenu() {
    if (!isOpen) {
        menu.classList.remove("hidden");
        menu.focus();
    } else {
        menu.classList.add("hidden");
    }
    menu.animate(
        { opacity: [0, 1] },
        {
            easing: "ease-in",
            duration: 100,
        },
    );

    isOpen = !isOpen;
}

function replaceCurrentElement(currentElement, newElement, newIndex) {
    let checkMark = createCheckMark();

    button.removeChild(currentElement);
    button.insertBefore(
        newElement.children[0].cloneNode(true),
        button.children[0],
    );
    
    
    menuItems.forEach((item) => {
        if (item.dataset.selected === "true") {
            item.removeChild(item.children[item.children.length - 1]);
        }
        item.dataset.selected = "false";
    });

    newElement.dataset.selected = "true";
    newElement.appendChild(checkMark);
}

const button = document.querySelector(".button");
const menu = document.querySelector(".menu");
const dropdown = document.querySelector(".dropdown");
const menuItems = menu.querySelectorAll("li");

let currentElementIndex = 0;
let isOpen = !menu.classList.contains("hidden");

menu.addEventListener("keydown", (event) => {
    if (event.key === "Enter") {
        let target = menuItems[currentElementIndex];
        let cur = button.children[0];
        replaceCurrentElement(cur, target);
    }

    if (event.key === "ArrowUp" && currentElementIndex !== 0) {
        currentElementIndex--;
    }

    if (event.key === "ArrowDown" && currentElementIndex < menuItems.length - 1) {
        currentElementIndex++;
    }
    menuItems[currentElementIndex].focus();
});

button.addEventListener("keydown", (event) => {
    event.preventDefault();
    if (event.key === "Enter") {
        closeMenu();
    }
});

button.addEventListener("click", (event) => {
    event.preventDefault();
    closeMenu();
});

menuItems.forEach((item, index) => {
    item.addEventListener("click", (event) => {
        event.preventDefault();
        let target = event.currentTarget;
        let item = target.children[0].cloneNode(true);

        button.removeChild(button.children[0]);
        button.insertBefore(item, button.children[0]);

        
        menuItems.forEach((item) => {
            if (item.dataset.selected === "true") {
                item.removeChild(item.children[item.children.length - 1]);
            }
            item.dataset.selected = "false";
        });

        target.dataset.selected = "true";

        let checkMark = createCheckMark();
        target.appendChild(checkMark);
    });
});

document.addEventListener("click", (event) => {
    let isClickedInside = dropdown.contains(event.target);
    if (!isClickedInside) {
        isOpen = false;
        menu.classList.add("hidden");
    }
});






