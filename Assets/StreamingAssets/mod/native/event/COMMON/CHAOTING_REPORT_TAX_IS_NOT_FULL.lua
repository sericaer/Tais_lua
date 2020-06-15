trigger = function()
    return  gm_data().date.month == 12 and gm_data().date.day == 30
                and  gm_data().chaoting.year_report_tax < gm_data().chaoting.year_expect_tax
end

occur_days = function ()
    return 3
end

options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_data().chaoting.prestige = gm_data().chaoting.prestige - 5
            gm_data().chaoting.power_party.prestige = gm_data().chaoting.power_party.prestige - 2
        end,

        tooltip = function ()
            return {{'CHAOTING_PRESTIGE_CHANGED', -5},
                    {'POWER_PARTY_PRESTIGE_CHANGED', -3}}
        end
    }
}