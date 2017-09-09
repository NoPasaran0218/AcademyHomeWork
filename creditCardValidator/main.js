var cardValidator = {
    isValid: function (str) {
        var reg = /\d{4}[-]\d{4}[-]\d{4}[-]\d{4}/;
        var originCardNumber = str;
        if (!str.match(reg))
            return {
                valid: false,
                number: originCardNumber,
                error: "wrong format"
            };
        str = str.replace(/[-]/g, '');
        //return str;
        var sameNumbersIdn = true;

        //перевірка на однаковість цифр
        for (var i = 1; i < str.length; i++) {
            if (str[i] !== str[0]) {
                sameNumbersIdn = false;
                break;
            }
        }

        if (sameNumbersIdn)
            return {
                valid: false,
                number: originCardNumber,
                error: "wrong format: all symbols are same"
            };

        //last digit%2
        if (+str[str.length - 1] % 2 != 0)
            return {
                valid: false,
                number: originCardNumber,
                error: "wrong format: last digit is odd"
            };

        var numbers = str.split('');
        var sum = numbers.reduce(function (memo, item) {
            return memo + +item;
        }, 0);

        if (sum <= 16)
            return {
                valid: false,
                number: originCardNumber,
                error: "wrong format: sum off all less then 16"
            };

        
        //luna algorithm
        //valid example "5168-7573-1879-6788"
        var lunaAlgoritmSum = numbers.reduce(function (previousValue, currentItem, index) {
            if (index % 2 == 0) {
                var res = +currentItem * 2;
                if (res > 9) {
                    res = res - 9;
                }

                return previousValue + res;
            } else {
                return previousValue + +currentItem;
            }
        }, 0)

        if (lunaAlgoritmSum % 10 !== 0) {
            return {
                valid: false,
                number: originCardNumber,
                error: "wrong format: doesn't match the algorithm of the luna "
            };
        }

        return {
            valid: true,
            number: originCardNumber
        };

    }
}