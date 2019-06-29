# Simple Data Notation (SDN)

Simple Data Notation started as a project to create a library that used regularexpressions that could be used in small projects. The initial setup for SDN was a simple setup without nesting. This is a more robust version with nesting. SDN was not created with amazing performance in mind, It is a test project to experiement with regex with a gaol in mind.
SDN builds a dll that you can link into your .net projects
`SDN is made for .netcore 2.2`

## performance

testing is done with this [file](test/SDN.txt)

> the avarage performance seems ok, But the first time a document is parsed or a value is read the time to execute is much higher.

Test (done 2000 times)  | Result in seconds | Avarage in seconds
---- | ------ | -------
read nesting value | 00.0584721s | 00.0000325s
read non nested value | 00.0270664s | 00.0000135s
parse test file | 01.1476503s | 00.0006127s


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
