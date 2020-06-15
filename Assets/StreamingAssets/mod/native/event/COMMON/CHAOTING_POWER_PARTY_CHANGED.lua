options = 
{
    OPTION_1 = 
    {
        selected = function ()
            gm_data().chaoting.power_party = gm_data().chaoting.other_partys:first()
        end,

        Tooltip = function ()
            return {{'POWER_PARY_CHANGED', gm_data().chaoting.other_partys:first().name}}
        end
    }
}