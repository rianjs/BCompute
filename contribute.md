How to contribute

## Development philosophy
I'm not a control freak... If you follow these guidelines, I'll give you commit access to the repository.

###Favor speed over memory usage
Sequence complements

### Don't compute it unless you have to; but if you do, collect as much useful information as you can along the way
This means that if there's a way to collect additional information along the way to coming up with some answer, you should compute it, and cache it, so it never has to be computed again.

#### Examples
* Silly example: the way I compute the probability of allele expression in a given offspring has the benefit of computing the probability of any given parent combination along the way; this was a nice side effect that was no slower than just applying the standard formula.
* More significantly, the way a reverse complement is computed is a two step process. The first step is to generate the complement. This complement is then stored, so if the user asks for it, it's an in-memory lookup instead. This is not a big deal with small sequences, but with sequences of tens of millions of base pairs, this performance advantage can be huge.

### Do the little things right
Examples:
* Use the knowledge that exists in your context to your advantage. If you know how many elements a container such as a `List` or `Dictionary` will hold--or at least know a reasonable upper bound--specify it when you instantiate it, because it will save unnecessary memory re-allocation and garbage collection.
* Don't hard-code data that can be derived from other data. For example, when converting DNA to RNA
* Don't re-derive data that can be maintained across conversions. Examples:
** When converting from DNA to RNA, there is no reason to re-count the elements of the sequence, just convert the thymine count to the uracil count.
** When converting a complement, you can swap the elements of the sequence without re-counting.

#### Don't try to jam a square peg into a round hole
If you're contorting your code to get it to work, the way you're modeling the domain is probably flawed. If you get the basic building blocks right, the rest will fall into line. And make sure you take a look at the domain primitives I've already written. I learned some of these lessons the hard way.

### Write unit tests
I use NUnit. It's awesome. If your contribution isn't unit tested, I won't accept it. Being a library, unit testing is pretty straightfoward.

If you're not a unit test expert, send me a PR anyway, and I'll help you write some tests, or point you to where I applied a pattern that might be helpful for you.

### Document your API
At the very least, write good summaries for your public methods so that users get the Intellisense description.

## Coding conventions
For the most part, conventions follow Microsoft's Framework Design Guidelines, . They are encapsulated in the `DotSettings` file in the /contribute top-level directory, so if you're using Visual Studio, they'll guide to you do the right thing without too much thinking.

Here's a short list that covers the bread-and-button conventions:
* Braces on new-lines, Allman style http://en.wikipedia.org/wiki/Indent_style#Allman_style
* Braces on conditionals, even if they're just one line
* Enum names should be singular. E.g. `Nucleotide`, not Nucleotides
* UpperCamelCase for all public fields and properties (both static and instance fields!)
* _lowerCamelCase for all private variables (both static and instance variables!)
* lowerCamelCase for all local variables
* Use signed primitives as public return values
* Use `var` to declare local variables, unless:
** Using `var` will do the wrong thing. For example: `ISet<string> foo = new HashSet<string>()` might make more sense for your use case.
** Using it results in less readable code.

## Common pitfalls for scientists who program
* `k`, `m`, and `n` are not good variable names. Take pity on your future self (and me!), and use descriptive variable, method, and object names. 
* If you're having trouble naming something, you're probably trying to do too much. Break up that object or method until you can name the one thing that it is or does.