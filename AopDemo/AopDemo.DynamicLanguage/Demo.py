class Person:
    """A simple class representing a person"""
    def __init__(self, name, age):
        self.name = name
        self.age = age

    def say_hello(self):
        print "Hello, my name is {}, I'm {} years old".format(self.name, self.age)

    def say_goodbye(self, recipient):
        print "Good bye, {}".format(recipient)

# create a new instance of Person and call the say_hello and say_goodbye methods
person = Person("Lorenzo Von Matterhorn", 35)

person.say_hello()
person.say_goodbye("Ms Goat")

old_say_hello = person.say_hello

def new_say_hello():
    print "Invoking new_say_hello"
    old_say_hello()
    print "Invoked new_say_hello"

# swap out the original implementation of say_hello
person.say_hello = new_say_hello
person.say_hello()