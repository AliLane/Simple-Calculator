using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Microsoft.Phone.Controls;

/*
 * This project has been added to GitHub on 6/6/2012
 */
namespace Calculator
{
    public partial class MainPage : PhoneApplicationPage
    {
        float number1, number2;
        string operation;
        bool firstZero = false, decimalEntered = false, nonZeroNumberPreceed = false;
        string inputTextString = "";
        int numberOfDigitsThatFitOnTextBlock = 12;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ac_button_Tap(object sender, GestureEventArgs e)
        {
            clear_text();
        }

        private void plus_button_Tap(object sender, GestureEventArgs e)
        {
            if (inputTextString.Length > 0)
            {
                update_text(" + ", 0);
                newNumber();
            }
        }

        private void minus_button_Tap(object sender, GestureEventArgs e)
        {
            if (validOperatorEntry("-") == 1)
                newNumber();
        }

        private void multiply_button_Tap(object sender, GestureEventArgs e)
        {
            if (inputTextString.Length > 0)
            {
                update_text(" * ", 0);
                newNumber();
            }
        }

        private void division_button_Tap(object sender, GestureEventArgs e)
        {
            if (inputTextString.Length > 0)
            {
                update_text(" / ", 0);
                newNumber();
            }
        }

        private void backspace_button_Tap(object sender, GestureEventArgs e)
        {
            backspace();
        }

        private void zero_button_Tap(object sender, GestureEventArgs e)
        {
            if (validateZerosEntry())
            {
                update_text("0", 0);
            }
        }
        
        private void one_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("1", 0);
        }

        private void two_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("2", 0);
        }

        private void three_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("3", 0);
        }

        private void four_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("4", 0);
        }

        private void five_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("5", 0);
        }

        private void six_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("6", 0);
        }

        private void seven_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("7", 0);
        }

        private void eight_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("8", 0);
        }

        private void nine_button_Tap(object sender, GestureEventArgs e)
        {
            if (!decimalEntered)
                nonZeroNumberPreceeds();
            update_text("9", 0);
        }

        private void equal_button_Tap(object sender, GestureEventArgs e)
        {
            update_text(calculateResult().ToString(), 1);
        }

        private void dot_button_Tap(object sender, GestureEventArgs e)
        {
            bool appendZero = false;
            if (inputTextString.Length == 0)
                appendZero = true;
            else if (inputTextString.Substring(inputTextString.Length - 1).Equals(" "))
                appendZero = true;
            else if (inputTextString.Substring(inputTextString.Length - 1).Equals("."))
            {
                backspace();
            }
            if (appendZero)
                update_text("0.", 0);
            else
                update_text(".", 0);
            decimalEntered = true;
        }

        private float calculateResult()
        {
            try
            {
                List<string> splitList = new List<string>();
                splitList = inputTextString.Split(' ').ToList<string>();
                if (splitList.Count > 1)
                {
                    number1 = float.Parse(splitList[0]);
                    operation = splitList[1].Trim();
                    number2 = float.Parse(splitList[2]);
                    number1 = performOperation(operation);
                    if (splitList.Count > 3)
                    {
                        for (int i = 3; i < splitList.Count; i = i + 2)
                        {
                            operation = splitList[i].Trim();
                            number2 = float.Parse(splitList[i + 1]);
                            number1 = performOperation(operation);
                        }
                    }
                    return number1;
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        private float performOperation(string operation)
        {
            switch (operation)
            {
                case "+":
                    return number1 + number2;
                case "-":
                    return number1 - number2;
                case "*":
                    return number1 * number2;
                case "/":
                    return number1 / number2;
                default:
                    return 0;
            }
        }

        private void update_text(string s, int mode)
        {
            // mode 0 => append
            // mode 1 => set
            if (mode == 0)
                inputTextString += s;
            if (mode == 1)
                inputTextString = s;
            if (inputTextString.Length > numberOfDigitsThatFitOnTextBlock)
            {
                input_textblock.FontSize = input_textblock.FontSize / 2;
                numberOfDigitsThatFitOnTextBlock *= 2;
            }
            input_textblock.Text = inputTextString;
        }

        private void clear_text()
        {
            update_text("", 1);
        }

        private void backspace()
        {
            if (inputTextString.Length > 0)
            {
                // if char being deleted is a space means we have to delete the operator and a space that preceed it
                if (inputTextString.Substring(inputTextString.Length - 1, 1).Equals(" "))
                {
                    update_text(inputTextString.Substring(0, inputTextString.Length - 3), 1);
                    // now we have to set the firstZero, decimalEntered, nonZeroNumberPreceed as per the previous number
                    setPreviousNumberProperties();
                }
                else
                {
                    if (inputTextString.Substring(inputTextString.Length - 1, 1).Equals("."))
                    {
                        decimalEntered = false;
                    }
                    if (inputTextString.Substring(inputTextString.Length - 1, 1).Equals("0"))
                    {
                        if (!decimalEntered && firstZero)
                            firstZero = false;
                    }
                    update_text(inputTextString.Substring(0, inputTextString.Length - 1), 1);
                }

                if (inputTextString.Substring(inputTextString.Length - 1, 1).Equals(" ") || inputTextString.Length == 0)
                {
                    newNumber();
                }
            }
        }

        private void setPreviousNumberProperties()
        {
            // set the firstZero, decimalEntered, nonZeroNumberPreceed as per the previous number

            // get previous number; read until a space character is encountered
            if (inputTextString.Length > 0)
            {
                int lastSpacePosition = inputTextString.LastIndexOf(' ');
                string previousNumberString = inputTextString.Substring(lastSpacePosition);
                if (previousNumberString.Contains("."))
                    decimalEntered = true;
                else
                {
                    nonZeroNumberPreceed = true;
                    firstZero = false;
                }
                if (decimalEntered)
                {
                    if (inputTextString[lastSpacePosition + 1].Equals("0"))
                        firstZero = true;
                }
            }
            else
            {

                decimalEntered = false;
                nonZeroNumberPreceed = false;
            }
        }

        private bool validateZerosEntry()
        {
            /* Acceptable
             * 1. Multiple zeros after a non zero number
             * 2. Multiple zeros after a decimal
             * 3. First zero entered (can be followed by a decimal)
             */

            /* Unacceptable
             * 1. Number starting with zero; followed by multiple zeros
             */

            if (decimalEntered)
                return true;

            if (!firstZero)
            {
                if (nonZeroNumberPreceed)
                    return true;
                firstZero = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void newNumber()
        {
            // This method resets the firstZero = false, decimalEntered = false, nonZeroNumberPreceed = false
            firstZero = false;
            decimalEntered = false;
            nonZeroNumberPreceed = false;
        }

        private void nonZeroNumberPreceeds()
        {
            if (!nonZeroNumberPreceed && !firstZero)
            {
                nonZeroNumberPreceed = true;
            }
            else if (firstZero)
            {
                backspace();
            }
        }

        private int validOperatorEntry(string operatorString)
        {
            if (inputTextString.Length > 0)
            {
                update_text(" " + operatorString + " ", 0);
                return 1;
            }
            else
            {
                update_text(operatorString, 0);
                return 0;
            }
        }

        /*********** FUTURE WORK
         * 1. Add parenthesis button ()
         * 2. Add +/- toggle button. Eg. make 12 ==> -12 or make -36 ==> 36
         */
    }
}