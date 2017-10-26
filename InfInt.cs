using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfInt
{
    /// <summary>
    /// class of Infinite integer
    /// </summary>
    class InfInt //: IComparable<InfInt>
    {
        List<int> infInt = new List<int>();
        int DefListMax; //operated on in Padding method
        int lCounter = 0;
        int realStringLength = 0;
        bool listIsNeg = false;
        bool negativeOutcome = false;
        bool plusUsed = false;
        bool minusUsed = false;
        bool timesUsed = false;
        public int listMax = 0;
        /// <summary>
        /// default constructor
        /// </summary>
        public InfInt()
        {
        }
        /// <summary>
        /// explicit value constructor
        /// </summary>
        /// <param name="anyNumberInput"></param>
        public InfInt(string anyNumberInput)
        {
            //operate on string as to get absolute value and remove '-' if necessary
            string realNumberString = anyNumberInput ;
            if (anyNumberInput[0]=='-')
            {
                listIsNeg = true;
                realNumberString = anyNumberInput.Substring(1);
            }
            //string without - sign
            realStringLength = realNumberString.Length;
            for (lCounter = 0; lCounter< realStringLength; lCounter++)
            {

                infInt.Add(int.Parse(Convert.ToString(realNumberString[lCounter])));
            }
            listMax = infInt.Count();
            infInt.Reverse();
        }
        /// <summary>
        /// Method adds the int items of the 2nd list into the 1st list with appropriate carries
        /// Accounts for (pos+pos), (neg + pos), (pos + neg), (neg + neg)
        /// will sometimes call the standard minus method for equivalent expressions. Saves repetition.
        /// </summary>
        /// <param name="lData"></param>
        /// <returns></returns>
        public int Plus(InfInt lData)
        {
            plusUsed = true;
            this.Padding(lData, timesUsed);
            //check pos (postive + posinput) | Normal addition rules
            if (!listIsNeg && !lData.listIsNeg)
            {
                for (lCounter=0; lCounter < DefListMax; lCounter++)
                {
                    if ((infInt[lCounter] + lData.infInt[lCounter]) <= 9)
                        infInt[lCounter] += lData.infInt[lCounter];
                    else
                    {
                        infInt[lCounter] += lData.infInt[lCounter] - 10;
                        infInt[lCounter + 1] += 1;
                    }
                }
            }
            // check (pos + neginput) | Normal subtraction rules, call .Minus
            else if(!listIsNeg && lData.listIsNeg)
            {
                plusUsed = false;
                lData.listIsNeg = false;
                this.Minus(lData);
            }
            // check(negative + neginput) | Normal addition rules
            else if(listIsNeg && lData.listIsNeg)
            {
                for (lCounter = 0; lCounter < DefListMax; lCounter++)
                {
                    if ((infInt[lCounter] + lData.infInt[lCounter]) <= 9)
                        infInt[lCounter] += lData.infInt[lCounter];
                    else
                    {
                        infInt[lCounter] += lData.infInt[lCounter] - 10;
                        infInt[lCounter + 1] += 1;
                    }
                }
            }
            // current list is negative (negative + input)
            else
            {
                negativeOutcome = CheckNegOutcome(lData.listMax, listMax, lData.infInt, infInt);
                //will get positive Outcome
                if (negativeOutcome == false)
                {
                    for (lCounter = 0; lCounter < DefListMax; lCounter++)
                    {
                        if (lData.infInt[lCounter] - infInt[lCounter] < 0)
                        {
                            infInt[lCounter] = lData.infInt[lCounter] - infInt[lCounter] + 10;
                            lData.infInt[lCounter+1] -= 1;
                        }
                        else
                            infInt[lCounter] = lData.infInt[lCounter] - infInt[lCounter];
                    }
                    listIsNeg = false;
                }
                //will get negative Outcome, i.e. normal subtraction rules
                else
                {
                    for (lCounter = 0; lCounter < DefListMax; lCounter++)
                    {
                        if (infInt[lCounter] - lData.infInt[lCounter] < 0)
                        {

                            infInt[lCounter] = infInt[lCounter]- lData.infInt[lCounter] + 10;
                            infInt[lCounter + 1] -= 1;
                        }
                        else
                            infInt[lCounter] -= lData.infInt[lCounter];
                    }
                    listIsNeg = true;
                }

            }
            listMax = infInt.Count();
            return 0;
        }
        /// <summary>
        /// Method subtracts the int items of the 2nd list into the 1st list with appropriate carries
        /// Accounts for (pos-pos), (neg - pos), (pos - neg), (neg - neg)
        /// May call the standard .Plus method for equivalent expressions. Saves repetition.
        /// </summary>
        /// <param name="lData"></param>
        /// <returns></returns>
        public int Minus(InfInt lData)
        {
            minusUsed = true;
            // decide if the outcome number will be negative
            negativeOutcome = CheckNegOutcome(listMax, lData.listMax, infInt, lData.infInt);
            this.Padding(lData, timesUsed);

            if (!listIsNeg && !lData.listIsNeg)// (positive - posinput)
            {
                //The Outcome will be negative, so reverse subtraction rules
                if (negativeOutcome == true)
                {
                    for (lCounter = 0; lCounter < DefListMax; lCounter++)
                    {
                        if (lData.infInt[lCounter] - infInt[lCounter] < 0)
                        {
                            infInt[lCounter] = lData.infInt[lCounter] - infInt[lCounter] + 10;
                            lData.infInt[lCounter + 1] -= 1;
                        }
                        else
                            infInt[lCounter] = lData.infInt[lCounter] - infInt[lCounter];
                    }
                    listIsNeg = true;
                }
                //The Outcome will be positive, so normal subtraction rules
                else
                {
                    for (lCounter = 0; lCounter < DefListMax; lCounter++)
                    {
                        if (infInt[lCounter] - lData.infInt[lCounter] < 0)
                        {
                            infInt[lCounter] = infInt[lCounter] - lData.infInt[lCounter] + 10;
                            infInt[lCounter + 1] -= 1;
                        }
                        else
                            infInt[lCounter] -= lData.infInt[lCounter];
                    }
                    listIsNeg = false;
                }
            }
            else if (!listIsNeg && lData.listIsNeg) // (positive - negative) | Standard Addition Rules pos + pos
            {
                lData.listIsNeg = false;
                this.Plus(lData);
            }
            else if(listIsNeg && !lData.listIsNeg) // is negative (negative - posinput) Addition rules
            {
                for (lCounter = 0; lCounter < DefListMax; lCounter++)
                {
                    if ((infInt[lCounter] + lData.infInt[lCounter]) <= 9)
                        infInt[lCounter] += lData.infInt[lCounter];
                    else
                    {
                        infInt[lCounter] = infInt[lCounter] + lData.infInt[lCounter] - 10;
                        infInt[lCounter + 1] += 1;
                    }
                }
                
            }
            else // (neg - neginput) | Neg + pos Addition rules
            {
                lData.listIsNeg = false;
                this.Plus(lData);
            }
            return 0;
        }
        /// <summary>
        /// Times Method is the overhead method: Decides whether the outcome is negative or positive
        /// Uses .Multiply method to solve expression.
        /// Accounts for (pos*pos), (neg*pos), (pos*neg), (neg*neg)
        /// </summary>
        /// <param name="lData"></param>
        /// <returns></returns>
        public int Times(InfInt lData)
        {
            //Postive Outcome
            if ((!listIsNeg && !lData.listIsNeg) || (listIsNeg && lData.listIsNeg))
            {
                this.Multiply(lData);
                listIsNeg = false;
                return 0;
            }
            //Negative Outcome
            else if((!listIsNeg && lData.listIsNeg) || (listIsNeg && !lData.listIsNeg))
            {
                this.Multiply(lData);
                listIsNeg = true;      
                return 0;
            }
            //Invalid inputs
            else
            {
                Console.WriteLine("Invalid Numbers for Operation!");
                return 0;
            }

        }
        /// <summary>
        /// The actual work done for .Times
        /// .Multiply Method multiplies the int items of the 2nd list into the 1st list with appropriate carries
        /// Accounts for (pos*pos), (neg*pos), (pos*neg), (neg*neg)
        /// </summary>
        /// <param name="lData"></param>
        private void Multiply(InfInt lData)
        {
            timesUsed = true;
            List<int> tempList1 = new List<int>();
            List<int> tempListTotal = new List<int>();
            int lCounter2;
            this.Padding(lData, timesUsed);


            //Pad the total'd amount with max amount of zeros needed
            for (lCounter = 0; lCounter <= (DefListMax * 2); lCounter++)
            {
                tempList1.Add(0);
                tempListTotal.Add(0);
            }
            //Goes through the 2nd list
            for (lCounter = 0; lCounter < DefListMax; lCounter++)
            {
                //Goes through the 1st list; Skips the index of the previous interation of list1
                for (lCounter2 = 0; lCounter2 < DefListMax; lCounter2++)
                {
                    tempList1[lCounter2 + lCounter] += (lData.infInt[lCounter] * infInt[lCounter2]);
                    tempList1[lCounter2 + lCounter + 1] += (tempList1[lCounter2 + lCounter] / 10);
                    tempList1[lCounter2 + lCounter] %= 10;
                }
                // Add into a temporary Total Accumulation list
                for (lCounter2 = lCounter; lCounter2 < DefListMax * 2; lCounter2++)
                {
                    if ((tempListTotal[lCounter2] + tempList1[lCounter2]) <= 9)
                    {
                        tempListTotal[lCounter2] += tempList1[lCounter2];
                        tempList1[lCounter2] = 0;
                    }
                    else
                    {
                        tempListTotal[lCounter2] += tempList1[lCounter2] - 10;
                        tempListTotal[lCounter2 + 1] += 1;
                        tempList1[lCounter2] = 0;
                    }
                }
            }
            // Overwrites the values in List1 with the values from the temporaryListTotal
            for (lCounter = 0; lCounter < DefListMax * 2; lCounter++)
            {
                infInt[lCounter] = tempListTotal[lCounter];
            }
        }
        /// <summary>
        /// Override ToString to allow objects of type InfInt to be printed in console.write
        /// </summary>
        /// <param name="infInt"></param>
        /// <returns></returns>
        public override string ToString()
        {
            var listOfStrings = new List<string>();
            //Convert list1 to string list
            for (int tempMax = listMax-1;tempMax>=0;tempMax--)
            {                
                listOfStrings.Add(Convert.ToString(infInt[(tempMax)]));
            }
            // Trim off excess zeros
            while (listOfStrings[0] == "0")
            {
                if (listOfStrings.Count != 1)
                {
                    listOfStrings.RemoveAt(0);
                }
                else break;
            }
            //return a (-) in front of Joined string if it the list is negative
            if(listIsNeg)
            {
                string negChar = "-";
                string returnedValue = negChar + String.Join("", listOfStrings);
                if (returnedValue == "-0")
                {
                    returnedValue = "0";
                }
                return returnedValue;
            }
            //return Joined string
            else
            {
                string returnedValue = String.Join("", listOfStrings);
                return returnedValue;
            }

        }
        /// <summary>
        /// Compares list1 and list2 to see if the outcome will be Negative when adding or subtracting
        /// Basically my own CompareTo method
        /// </summary>
        /// <param name="listMax1"></param>
        /// <param name="listMax2"></param>
        /// <param name="infInt1"></param>
        /// <param name="infInt2"></param>
        /// <returns></returns>
        static private bool CheckNegOutcome(int listMax1, int listMax2, List<int> infInt1, List<int> infInt2 )
        {
            if (listMax1 == listMax2)
            {
                for (int i = listMax1-1; i >= 0; i--)
                {
                    if (infInt1[i] < infInt2[i])
                        return true;
                    if (infInt1[i] > infInt2[i])
                    {
                        return false;
                    }
                }
                return false;
            }
            else if (listMax2 > listMax1)
                return true;
            else
                return false;          
        }
        /// <summary>
        /// This method:
        /// if not multiplying, will pad the list having less digits with zeros on the left hand side
        /// If multiply, will pad both lists until each are the same number of digits(2*Max+1).
        ///     Example: (2digit) 99 * 99 = 9801 (4 digit)
        ///     Excess zeros will be trimmed at the end before displaying on screen
        /// </summary>
        /// <param name="lData"></param>
        private void Padding(InfInt lData, bool timesUsed)
        {
            DefListMax = (listMax > lData.listMax ? listMax : lData.listMax);
            //If adding or subtracting
            if (!timesUsed)
            {
                if (listMax > lData.listMax)// Pad list2 with 0s
                {
                    for (lCounter = lData.listMax; lCounter <= listMax; lCounter++)
                    {
                        lData.infInt.Add(0);
                    }
                    infInt.Add(0);
                }
                else if (listMax < lData.listMax)// Pad list1 with 0s
                {
                    for (lCounter = listMax; lCounter <= lData.listMax; lCounter++)
                    {
                        infInt.Add(0);
                    }
                    lData.infInt.Add(0);
                }
                else// they are equal; Pad them both once for safety from indexing error
                {
                    infInt.Add(0);
                    listMax += 1;
                    lData.infInt.Add(0);
                }
            }
            else // If multiplying, pad until both lists are equal to 2*(the greater indexed list)
            {
                while (listMax <= (DefListMax*2))
                {
                    infInt.Add(0);
                    listMax++;
                }
                while(lData.listMax <= (DefListMax*2))
                {
                    lData.infInt.Add(0);
                    lData.listMax++;
                }
            }
        }
    }
}