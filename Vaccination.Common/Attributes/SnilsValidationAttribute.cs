using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Vaccination.Common.Attributes
{
    public class SnilsValidationAttribute : ValidationAttribute
    {
        /*
            https://ru.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BD%D1%82%D1%80%D0%BE%D0%BB%D1%8C%D0%BD%D0%BE%D0%B5_%D1%87%D0%B8%D1%81%D0%BB%D0%BE

            1. Проверка контрольного числа Страхового номера проводится только для номеров больше номера 001-001-998
            2. Контрольное число СНИЛС рассчитывается следующим образом:
            2.1. Каждая цифра СНИЛС умножается на номер своей позиции (позиции отсчитываются с конца)
            2.2. Полученные произведения суммируются
            2.3. Если сумма меньше 100, то контрольное число равно самой сумме
            2.4. Если сумма равна 100 или 101, то контрольное число равно 00
            2.5. Если сумма больше 101, то сумма делится по остатку на 101 и контрольное число определяется остатком от деления аналогично пунктам 2.3 и 2.4
        */

        public const int SNILS_LENGTH = 11;
        public const int SNILS_NUMBER_LENGTH = 9;
        public const int SNILS_CHECKSUM_LENGTH = 2;
        public const int SNILS_MIN_NUMBER_TO_CHECK = 1001998;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var snils = (string)value;
            snils = snils.Trim();

            snils = String.Join("", snils.Where(char.IsDigit));

            if (snils.Length != SNILS_LENGTH)
            {
                return new ValidationResult("Snils length must be 11 digits");
            }

            var number = snils.Substring(0, SNILS_NUMBER_LENGTH);
            var checksum = int.Parse(snils.Substring(SNILS_NUMBER_LENGTH, SNILS_CHECKSUM_LENGTH));

            if (int.Parse(number) <= SNILS_MIN_NUMBER_TO_CHECK)
            {
                return ValidationResult.Success;
            }

            var i = 9;
            var sum = 0;
            foreach(var n in number)
            {
                sum += i-- * (int)Char.GetNumericValue(n);
            }

            if (CheckControlSum(sum, checksum))
            {
                return ValidationResult.Success;
            }

            if (sum > 101)
            {
                var devided = sum % 101;

                if (CheckControlSum(devided, checksum))
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult("Incorrect snils checksum");
        }

        private bool CheckControlSum(int sum, int checksum)
        {
            return ((sum < 100 && sum == checksum) || ((sum == 100 || sum == 101) && checksum == 0));
        }
    }
}
