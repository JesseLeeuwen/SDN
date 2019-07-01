# Simple Data Notation (SDN)

Simple Data Notation started as a project to create a library that used regularexpressions that could be used in small projects. The initial setup for SDN was a simple setup without nesting. This is a more robust version with nesting. SDN was not created with amazing performance in mind, It is a test project to experiement with regex with a gaol in mind.
SDN builds a dll that you can link into your .net projects
`SDN is made for .netcore 2.2`

## performance

testing is done with this [file](test/SDN.txt)

Test (done 2000 times)  | Result in seconds | Avarage in seconds
---- | ------ | -------
parse test file | 00.4240570s | 00.0002120s
read nesting value | 00.4240570s | 00.0002120s
read non nested value | 00.0055955s | 0.0000028s


## SDN format
```
# strings: captures everything between ""
data: [
    "This is a piece of text",
    "this is a piece of text"
]

# nesting of objects
scene: [
    name: [menu]
    objects: [
        player: [
            name: [player1]
            lvl: [1]
            str: [10]
            int: [7]
            dex: [9]
            hp: [14]
            attack: [4]
            inventory: [10, 20, 33, 120] # simple array support
        ]
        frog_enemy: [
            name: [frog]
            lvl: [2]
            hp: [4]
            attack: [4]
        ]
    ]
]
```

## Features
- [x] nesting
- [x] capture everything between qoutes
- [x] get tokens with a path (scene.objects.player) 
- [ ] typing
- [ ] fix parse performance

## Takeaways
- do's and dont's with regular expressions
- making shared libraries with .netcore
- How to optimize a regular expression

## Credits

- https://www.rexegg.com/regex-optimizations.html
- https://www.loggly.com/blog/regexes-the-bad-better-best/


