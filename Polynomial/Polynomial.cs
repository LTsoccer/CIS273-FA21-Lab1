using System;
using System.Collections.Generic;

namespace Polynomial
{
    public class Polynomial
    {
        public int Degree => NumberOfTerms==0 ? 0 : terms.First.Value.Power;
            
        public int NumberOfTerms => terms.Count;
      
        protected LinkedList<Term> terms;

        public Polynomial()
        {
            terms = new LinkedList<Term>();
        }

        // Add a new term with the given c and p to this polynomial
        public void AddTerm(double coefficient, int power)
        {
            Term term = new Term(power, coefficient);

            if (terms.Count == 0)
            {
                terms.AddFirst(term);
            }
            else
            {
                LinkedListNode<Term> currentNode = terms.First;
                while (currentNode != null)
                {
                    // if existing term with same power
                    // then add coefficients
                    if (currentNode.Value.Power == term.Power)
                    {
                        currentNode.Value.Coefficient += term.Coefficient;
                        if (currentNode.Value.Coefficient == 0)
                        {
                            terms.Remove(currentNode);
                        }
                        return;
                    }

                    // if no exisiting term with same power
                    // then add new term at the right spot
                    if (currentNode.Value.Power < term.Power)
                    {
                        terms.AddBefore(currentNode, term);
                        return;
                    }

                    if(currentNode.Next == null )
                    {
                        terms.AddLast(term);
                        return;
                    }

                    currentNode = currentNode.Next;
                }
            }
        }
       


        public static Polynomial Add(Polynomial p1, Polynomial p2)
        {
            Polynomial sum = new Polynomial();

            // Add all terms from p1 to sum
            foreach (Term t in p1.terms)
            {
                sum.AddTerm(t.Coefficient, t.Power);
            }

            foreach( Term t in p2.terms)
            {
                sum.AddTerm(t.Coefficient, t.Power);
            }

            return sum;
        }


        public static Polynomial Subtract(Polynomial p1, Polynomial p2)
        {
            Polynomial difference = new Polynomial();

            // Add all terms from p1 to sum
            foreach (Term t in p1.terms)
            {
                difference.AddTerm(t.Coefficient, t.Power);
            }

            foreach (Term t in p2.terms)
            {
                difference.AddTerm(0 - t.Coefficient, t.Power);
            }

            return difference;
        }

        public static Polynomial Negate(Polynomial p)
        {
            Polynomial negate = new Polynomial();
            foreach (Term t in p.terms)
            {
                negate.AddTerm(0 - t.Coefficient, t.Power);
            }
            return negate;
        }

        public static Polynomial Multiply(Polynomial p1, Polynomial p2)
        {
            Polynomial product = new Polynomial();

            // Add all terms from p1 to sum
            foreach (Term t in p1.terms)
            {
                foreach (Term t2 in p2.terms)
                {
                    product.AddTerm(t.Coefficient * t2.Coefficient, t.Power + t2.Power);
                }
            }


            return product;
        }

        public override string ToString()
        {
            if (terms.Count == 0)
            {
                return "0";
            }
            string result = "";

            foreach( Term t in terms)
            {
                result += "+" + t.ToString();
            }

            return result;
        }

    }
}
