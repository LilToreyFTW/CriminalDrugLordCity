# Gameplay Systems Design

## Premise

The player builds an underground logistics empire in a fictional city by producing and distributing illegal but fictional contraband goods. The focus is on risk, territory, money flow, staff management, and coordinated expansion.

## Core Resource Model

### Resources

- `Cash`: liquid currency for wages, supply buys, bribes, repairs, and property upgrades
- `Influence`: unlocks suppliers, district perks, and higher-tier recruits
- `Heat`: district-level enforcement pressure caused by visible crime
- `Trust`: relationship score with suppliers, buyers, and crew leaders
- `Supply`: raw inputs required for production
- `Product`: finished fictional contraband goods

### Fictional Product Families

- `Rush Gel`: low-tier stimulant contraband with fast turnaround
- `Ghost Tabs`: compact high-margin social market item
- `Volt Vials`: fragile premium product with transport risks
- `Blank Chips`: forged access devices sold through specialist clients
- `Pulse Canisters`: bulky industrial contraband requiring warehouse-scale handling

Each product differs by:

- production station requirements
- batch time
- smell or visibility risk
- transport volume
- buyer demand band
- police attention multiplier

## Production System

## Player Loop

1. Acquire raw inputs from suppliers.
2. Deliver inputs to a property.
3. Assign production jobs to stations.
4. Monitor quality, contamination, breakdowns, and worker reliability.
5. Package finished goods into shippable units.

### Production Stations

- `Bench Station`: cheap, low-volume starter output
- `Mixer Rack`: medium throughput and recipe branching
- `Press Unit`: creates compact high-margin product forms
- `Refinement Tower`: late-game high-risk premium station
- `Forge Desk`: produces forged devices and identity-adjacent contraband

### Batch Variables

- input quality
- worker skill
- station cleanliness
- power stability
- district pressure
- player-selected recipe profile

### Output Grades

- `Scuffed`
- `Standard`
- `Clean`
- `Signature`

Higher grades increase:

- sale price
- client loyalty
- rival theft chance
- enforcement interest

### Recipe Discovery

Recipes are unlocked through:

- supplier tips
- salvaged notebooks
- employee experimentation
- buyer requests
- rare black-market auctions

Recipes alter:

- value
- yield
- durability during transport
- heat generated per sale
- specific market demand

### Failure States

- spoiled batches
- station fires
- toxic leaks
- power outages
- employee injury
- noisy production drawing attention

## Distribution System

## Channels

- `Hand-to-hand`: direct player sales for early-game cash and district knowledge
- `Dead drops`: low-contact hidden deliveries with timer pressure
- `Dealer crews`: AI staff assigned to corners, events, or routes
- `Courier routes`: vehicle-based bulk movement between properties and buyers
- `Wholesale clients`: larger scheduled deliveries with reputation gates

### Customer Model

Each district has hidden demand values for:

- cheap volume product
- premium discreet product
- party/social product
- industrial specialist product

Customers vary by:

- spending power
- schedule window
- loyalty
- snitch risk
- panic threshold during police presence

### Delivery Gameplay

- walking routes are discreet but slow
- skate routes are fast but physically risky
- vehicle routes move bulk but attract plate recognition and checkpoints

The player manages:

- route choice
- package size
- stash placement
- meeting timing
- escape options

### Dealer Management

Dealers have:

- courage
- greed
- discretion
- speed
- loyalty
- district familiarity

Dealers can:

- skim profits
- get arrested
- vanish with stock
- request backup
- increase local demand if protected

### Distribution Upgrades

- coded packaging lowers inspection risk
- burner network improves route flexibility
- hidden compartments increase vehicle carry capacity
- encrypted dispatch board increases courier efficiency

## Territory System

## District Control

Every neighborhood tracks:

- `Demand`
- `Police Presence`
- `Rival Presence`
- `Crew Presence`
- `Civilian Tension`
- `Property Value`

### Territory Actions

- seed demand with low-risk product
- intimidate rival crews
- bribe local intermediaries
- clean up visible chaos
- upgrade fronts to legit-looking businesses
- recruit local lookouts

### Territory States

- `Cold`: low sales, low risk
- `Open`: profitable but contested
- `Watched`: strong police focus
- `Locked`: rival or police dominance
- `Owned`: player-favored operational stability

### Front Businesses

Property fronts disguise activity and shape district response.

Examples:

- convenience shop
- auto garage
- print shop
- shipping office
- night bar

Fronts provide:

- passive laundering
- hidden storage
- employee slots
- route anchor points
- limited legal revenue

## Police Heat System

## Heat Layers

- `Personal Heat`: tied to player visibility and recent crimes
- `Property Heat`: tied to specific buildings
- `District Heat`: tied to repeated activity in one zone
- `City Heat`: major story-level crackdown meter

### Heat Sources

- open combat
- reckless driving
- visible exchanges
- repeated traffic to stash sites
- loud production failures
- captured crew testimony
- surveillance exposure

### Enforcement Escalation

1. patrol presence increases
2. stop-and-search events begin
3. undercover buyers appear
4. warrant service and property raids occur
5. task force operations lock down districts

### Counterplay

- rotate routes
- cool down districts
- bribe informants
- swap vehicles
- shut down hot properties
- use clean fronts
- destroy evidence before raids

### Investigation Gameplay

Police build cases over time through:

- camera sightings
- seized packages
- flipped associates
- financial irregularities
- traceable vehicles

Case progress should be partially visible so players feel pressure without perfect information.

## Rival System

## Rival Factions

Use 3 to 5 factions with different operating styles.

- `Dock Wolves`: bulk smugglers with strong port influence
- `Glass Saints`: premium social-market operators
- `Gravel Union`: violent industrial hijackers
- `Crown Meridian`: white-collar logistics manipulators

### Rival Behaviors

- undercut prices
- steal couriers
- bribe your dealers
- sabotage stations
- pressure suppliers
- launch drive-by attacks
- contest properties during weak periods

### Rival AI Goals

- maximize territory share
- disrupt your growth spikes
- retaliate when attacked
- exploit heat-heavy districts
- poach vulnerable staff

### Rival Pressure Meter

Every faction tracks attitude toward the player:

- `Ignoring`
- `Testing`
- `Hostile`
- `Vendetta`

This drives frequency and severity of encounters.

## Combat and Conflict

Conflict should be costly, not constant.

- fists are quiet and low-profile
- melee is fast and intimidating but risky
- firearms end fights quickly but spike heat dramatically
- damage to vehicles and properties creates persistent cost

Winning combat can:

- recover stolen product
- protect dealers
- defend territory
- interrupt raids

Losing combat can:

- drop inventory
- wound crew
- expose stash locations
- trigger hospital or jail recovery loops

## Staff and Automation

## Staff Roles

- `Cook/Tech`: runs production stations
- `Runner`: moves packages between stash points
- `Dealer`: handles street-level demand
- `Driver`: runs courier routes
- `Lookout`: warns of raids and rival movement
- `Manager`: boosts efficiency at larger properties

### Staff Stats

- efficiency
- loyalty
- nerve
- discretion
- wage expectation
- special perk

### Automation Ladder

1. player performs all work manually
2. single-room safehouse with one employee
3. specialized workshop with scheduled batches
4. multi-property route network
5. citywide semi-automated empire

Automation should reduce labor but increase systemic risk if one node fails.

## Co-op System

## Shared Roles

Co-op works best when players can specialize naturally.

- one player handles supply and production
- one runs sales and territory
- one acts as driver/security
- one manages heat cleanup and logistics

### Shared Systems

- shared cash pool with permission levels
- role-based property access
- shared route board
- shared stash inventories
- synchronized heat map
- revive and extraction loops

### Co-op Tension

To keep co-op engaging:

- police attention scales with group sloppiness
- rival attacks target the weakest active operation
- synchronized deliveries require timing
- crew trust drops if players overspend or abandon roles

### Co-op Progression

- unlock larger fronts requiring multiple active players
- chain missions with split objectives
- multi-vehicle convoy deliveries
- coordinated raid defense
- simultaneous district takeovers

## Mission Structure

## Mission Types

- supplier pickup
- timed courier drop
- recipe recovery
- dealer rescue
- witness intimidation
- warehouse defense
- police evidence intercept
- rival sabotage

Each mission should touch at least one major system and ideally two.

## Progression Arc

### Early Game

- small hand deliveries
- one safehouse
- one low-tier product family
- minor patrol pressure

### Mid Game

- multiple fronts
- dealer network
- rival retaliation
- recipe specialization
- property raids begin

### Late Game

- citywide logistics web
- premium product lines
- faction wars
- task force crackdowns
- co-op coordinated empire management

## Integration Notes For Current Project

The existing Unity project already has:

- missions
- DLC hooks
- police system
- vehicle systems
- NPC systems
- property-like scene building hooks

Recommended next implementation order:

1. add district data definitions for demand, heat, rival presence, and ownership
2. add product definitions with station requirements and risk modifiers
3. add property/front definitions with employee slots and storage
4. extend missions into supplier, courier, and raid events
5. connect police heat to player actions and delivery visibility
6. add rival faction simulation tick
7. add multiplayer-ready shared state boundaries
