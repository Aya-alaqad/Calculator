using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Calculator
{
    internal class clsCalculatorCore
    {
        private static byte getPriority(char op)
        {
            if (op == '-' || op == '+')
                return 1;
            else if (op == '*' || op == '/')
                return 2;
            else if (op == 'n')
                return 3;
            else return 0;

        }
        private static float doSingleCalculation(char op, float op1, float op2)
        {
            switch (op)
            {
                case '+':
                    op1 += op2;
                    break;
                case '-':
                    op1 -= op2;
                    break;
                case '*':
                    op1 *= op2;
                    break;
                case '/':
                    if (op2 == 0)
                        return float.NaN;
                    op1 /= op2;
                    break;
                case 'n':
                    op1 *= -1;
                    break;

            }
            return op1;
        }
        private static float calculateTop(Stack<float> operands, Stack<char> operators)
        {
            if (operands.Count == 0)
                return float.NaN;
            else if (operators.Count == 0)
                return float.NaN;

            char op = operators.Pop();
            if(op=='n')
            {
                return doSingleCalculation(op, operands.Pop(), 0);
            }
           
            
            else
            {
                float op2 = operands.Pop();
                if (operands.Count == 0)
                    return float.NaN;

                return doSingleCalculation(op,operands.Pop(),op2);
            }
        }
        private static void handleRightParanthesis(Stack<float> operands, Stack<char> operators)
        {
            float res;          
            while (operators.Count != 0 && operands.Count != 0 && operators.Peek() != '(')
            {
                if (operators.Peek() == '(')
                    continue;
                res = calculateTop(operands, operators);
                operands.Push(res);
                if (float.IsNaN(res))
                {
                    return;
                }
            }
            if (operators.Count!=0 && operators.Peek() == '(')
                operators.Pop();
        }
        private static int handleNumbers(Stack<float> operands, Stack<char> operators, int i, string s)
        {
            string n = "";
            float num;
            while (i < s.Length && (((int)s[i] - '0' <= 9 && (int)s[i] - '0' >= 0) || s[i] == '.'))
            {
                n += s[i];
                i++;
            }
            num = Convert.ToSingle(n);

            operands.Push(num);
            return --i;
        }
        private static void handleOperator(Stack<float> operands, Stack<char> operators, char op)
        {
            float res;
            while (operators.Count != 0 && getPriority(op) <= getPriority(operators.Peek()))
            {
                if (operators.Peek() == '(')
                    continue;
                res= calculateTop(operands, operators);
                operands.Push(res);
                if (float.IsNaN(res))
                {                 
                    return;
                }
            }
            operators.Push(op);
        }
        public static float calculateReslut(string s)
        {
            bool isThereError = false;
            if (s.Length == 0)
                return float.NaN;
            if (s[0]==')' || s[0]=='%')
            {
                return float.NaN;
            }
            
           
            Stack<float> operands = new Stack<float>();
            Stack<char> operators = new Stack<char>();

          
            float op2 = 0;
           
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '(')
                {
                    operators.Push(c);
                    //cases like 5(5) treat it as *;
                    if (i != 0 && (int)s[i - 1] - '0' >= 0 && (int)s[i - 1]-'0' <= 9)
                    {                       
                        handleOperator(operands, operators, '*');
                    }
                    if(operands.Count!=0&&float.IsNaN(operands.Peek()))
                    {
                        isThereError = true;
                        break;
                    }
                }

                else if (c == ')')
                {
                    handleRightParanthesis(operands, operators);
                    if(operands.Count!=0&&float.IsNaN(operands.Peek()))
                    {
                        isThereError = true;
                        break;
                    }
                    //cases like (5)(5) or (5)5 treat it as *;
                    if (i < s.Length - 1 && ( ((int)s[i + 1] - '0' >= 0 && (int)s[i + 1] - '0' <= 9) || s[i+1]=='('))
                        handleOperator(operands, operators, '*');
                    if (operands.Count != 0 && float.IsNaN(operands.Peek()))
                    {
                        isThereError = true;
                        break;
                    }
                }
                
                else if (c == '*' || c == '-' || c == '+' || c == '/')
                {
                    if (i == 0 || (s[i - 1] == '-' || s[i - 1] == '+' || s[i - 1] == '*' || s[i - 1] == '/') || s[i-1]=='(')
                    {
                        if (s[i] == '-')
                        {
                            operators.Push('n');
                        }
                        else if (s[i] == '+')
                            continue;
                        //positive number or expression 

                        else
                        {
                            //syntax error
                            isThereError = true;
                            break;
                        }
                        continue;
                    }
                    else
                    {
                        handleOperator(operands, operators, c);
                        if (operands.Count != 0 && float.IsNaN(operands.Peek()))
                        {
                            isThereError = true;
                            break;
                        }
                    }
                }

                else if ((int)c - '0' <= 9 && (int)c - '0' >= 0)
                {
                    i = handleNumbers(operands, operators, i, s);
                }
                else if(c=='%')
                {
                    if (i - 1 >=0 && ((int)s[i-1] - '0' > 9 || (int)s[i-1] - '0' < 0)&&s[i-1]!=')')
                        return float.NaN;
                    operands.Push(operands.Pop() / 100);
                }
            }

            if (isThereError)
                return float.NaN;

            while (operators.Count != 0)
            {
                if (operators.Peek() == '(')
                {
                    operators.Pop();
                    continue;
                }
               
                op2 = calculateTop(operands,operators);
                
                if (float.IsNaN(op2))
                {
                    isThereError=true;
                    break;
                }
                else
                    operands.Push(op2);

            }

            if (isThereError)
                return float.NaN;
            else if (operands.Count == 0)
                return float.NaN;
            else return operands.Pop();

        }
    }
}
